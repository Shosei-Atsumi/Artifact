import DbUtility
import Entity
from apiclient.discovery import build
import json
import re
import datetime

URL = 'https://www.googleapis.com/youtube/v3/'
# ここにAPI KEYを入力
API_KEY = ''
DEV_API_KEY1 = ''
DEV_API_KEY2 = ''

#ENV = 'dev'
ENV = 'prod'

def GetApiAuthYoutube():
   youtube = build('youtube', 'v3', developerKey=DEV_API_KEY1)
   return(youtube)

# 1 quota
def GetChannelInfoFromChannelId(dbUtility, youtube, channel_id):
   t_channel_info = Entity.Tables.t_channel_info(dbUtility)
   search_response = youtube.channels().list(
     id = channel_id,
     part = "id,snippet,statistics",
     fields =  "items(id,snippet(title),statistics(subscriberCount,videoCount,viewCount))"
   ).execute()

   for search_result in search_response.get("items", []):
        t_channel_info.channel_id = channel_id
        t_channel_info.channel_name = search_result["snippet"]["title"]
        t_channel_info.channel_subscribers = search_result["statistics"]["subscriberCount"]
        t_channel_info.channnel_video_count = search_result["statistics"]["videoCount"]
        t_channel_info.channel_video_view_count = search_result["statistics"]["viewCount"]

   return(t_channel_info)

def GetChannelInfo(youtube):
    dbUtility = DbUtility.DbUtility()
    channel_ids = dbUtility.SelectAll('t_channel', 'channel_id')
    
    for channel_id in channel_ids:
        t_channel_info = GetChannelInfoFromChannelId(dbUtility, youtube, channel_id[0])
        t_channel_info.serial = t_channel_info.GetNextSerial()
        t_channel_info.Insert()
        t_channel_info.Clear()
        

def GetVideoListFromChannelId(youtube, channel_id):
   nextPagetoken = None
   nextpagetoken = None
   videos = []
   c = 0
   while True:
       if nextPagetoken != None:
           nextpagetoken = nextPagetoken

       search_response = youtube.search().list(
         channelId = channel_id,
         part = "id,snippet",
         maxResults = 50, #最大50までしか取れないので最大値を指定
         order = "date", #日付順にソート
         pageToken = nextpagetoken, #再帰的に指定
         #fields =  "items(id,snippet(title,publishedAt))"
         ).execute()

       for search_result in search_response.get("items", []):
           if(search_result["id"]["kind"] == "youtube#video"):
               values = []
               values.append(search_result["id"]["videoId"])
               values.append(search_result["snippet"]["title"])
               values.append( re.sub('T|Z', ' ',search_result["snippet"]["publishedAt"]))
               values.append(channel_id)
               videos.append(values)
       c = c + 1
       if ENV == 'dev':
           break
       elif ENV == 'prod':
           try:
               nextPagetoken =  search_response["nextPageToken"] #次の結果がなくなるまで繰り返す
           except:
               break

   return(videos)

def GetVideoListFromChannelIdFromTo(youtube, channel_id, beforeAfterList):
   nextPagetoken = None
   nextpagetoken = None
   videos = []
   c = 0
   while True:
       if nextPagetoken != None:
           nextpagetoken = nextPagetoken

       search_response = youtube.search().list(
         channelId = channel_id,
         part = "id,snippet",
         maxResults = 50, #最大50までしか取れないので最大値を指定
         order = "date", #日付順にソート
         publishedAfter = beforeAfterList[0],
         publishedBefore = beforeAfterList[1],
         pageToken = nextpagetoken, #再帰的に指定
         #fields =  "items(id,snippet(title,publishedAt))"
         ).execute()

       for search_result in search_response.get("items", []):
           if(search_result["id"]["kind"] == "youtube#video"):
               values = []
               values.append(search_result["id"]["videoId"])
               values.append(search_result["snippet"]["title"])
               values.append( re.sub('T|Z', ' ',search_result["snippet"]["publishedAt"]))
               values.append(channel_id)
               videos.append(values)
       c = c + 1
       if ENV == 'dev':
           break
       elif ENV == 'prod':
           try:
               nextPagetoken =  search_response["nextPageToken"] #次の結果がなくなるまで繰り返す
           except:
               break

   return(videos)

def GetVideoList(youtube):
    dbUtility = DbUtility.DbUtility()
    channel_ids = dbUtility.SelectAll('t_channel', 'channel_id')
    
    afterBeforeList = GetAfterBefore()
    for afterBefore in afterBeforeList:
        for channel_id in channel_ids:
            videos = []

            videos = GetVideoListFromChannelIdFromTo(youtube, channel_id[0], afterBefore)
            #dbUtility.Insert('t_all_acquired_video_info', ['channel_id'], ["'" + channel_id[0] + "'"])

            for video in videos:
                if(len(video) == 0):
                    continue
                values = []
                res = dbUtility.SelectOne('t_video','video_id' ,dbUtility.AppendSql('video_id =', dbUtility.ConvertToDbStr(video[0])))
                
                if(res != None):
                    continue
                for value in video:
                    values.append(dbUtility.ConvertToDbStr(value))
                dbUtility.Insert('t_video', ['video_id', 'video_title', 'dt_video_upload', 'channel_id'], values)
    
    #for channel_id in channel_ids:
    #    exist = dbUtility.SelectOne('t_all_acquired_video_info', 'channel_id', dbUtility.AppendSql('channel_id =', dbUtility.ConvertToDbStr(channel_id[0])))
    #    videos = []
    #    if(exist == None):
    #        videos = GetVideoListFromChannelId(youtube, channel_id[0])
    #        dbUtility.Insert('t_all_acquired_video_info', ['channel_id'], ["'" + channel_id[0] + "'"])
    #    else:
    #        videos = GetVideoListFromChannelIdAndLimit(youtube, channel_id[0])

    #    for video in videos:
    #        if(len(video) == 0):
    #            continue
    #        values = []
    #        res = dbUtility.SelectOne('t_video','video_id' ,dbUtility.AppendSql('video_id =', dbUtility.ConvertToDbStr(video[0])))
    #        if(res != None):
    #            continue
    #        for value in video:
    #            values.append(dbUtility.ConvertToDbStr(value))
    #        dbUtility.Insert('t_video', ['video_id', 'video_title', 'dt_video_upload', 'channel_id'], values)


def GetVideoListFromChannelIdAndLimit(youtube, channel_id, limit = 10):
   videos = []
   search_response = youtube.search().list(
     channelId = channel_id,
     part = "id,snippet",
     maxResults = limit,
     order = "date", #日付順にソート
     fields =  "items(id,snippet(title,publishedAt))"
     ).execute()

   for search_result in search_response.get("items", []):
        values = []
        if(search_result["id"]["kind"] == "youtube#video"):
            values.append(search_result["id"]["videoId"])
            values.append(search_result["snippet"]["title"])
            values.append( re.sub('T|Z', ' ',search_result["snippet"]["publishedAt"]))
            values.append(channel_id)
            videos.append(values)

   return(videos)

def GetVideoInfoFromVideoId(youtube, video_id):
   info = []
   search_response = youtube.videos().list(
     id = video_id,
     part = "id,snippet,statistics",
     fields = "items(id,snippet(title),statistics(viewCount))",
     ).execute()

   for search_result in search_response.get("items", []):
        info.append(search_result["snippet"]["title"])
        info.append(search_result["statistics"]["viewCount"])

   return(info)

def GetVideoInfo(youtube):
    dbUtility = DbUtility.DbUtility()
    video_ids = dbUtility.SelectAll('v_video_info_update_target', 'video_id')

    for video_id in video_ids:
        info = GetVideoInfoFromVideoId(youtube, video_id[0])
        if(len(info) == 0):
            continue
        maxSerial = dbUtility.SelectOne('t_video_info','MAX(serial)' , dbUtility.AppendSql('video_id =', dbUtility.ConvertToDbStr(video_id[0])))

        if(maxSerial == None):
            maxSerial = 0
        
        maxSerial += 1
        values = []
        values.append(dbUtility.ConvertToDbStr(video_id[0]))
        values.append(dbUtility.ConvertToDbStr(str(maxSerial)))
        for value in info:
           values.append(dbUtility.ConvertToDbStr(value))
        dbUtility.Insert('t_video_info', ['video_id', 'serial', 'video_name', 'video_views'], values)

def GetAfterBefore(yeayCount = 10):
    now = datetime.datetime.now()
    year = now.year

    periodList = []
    periodList.append(year)

    afterBeforeList = []
    for counter in range(yeayCount):
        year = year -1
        periodList.append(year)

    for period in periodList:
        afterBeforeList.append(['{}-01-01T00:00:00Z'.format(period), '{}-12-31T00:00:00Z'.format(period)])

    return afterBeforeList

if __name__ == '__main__':
    youtube = GetApiAuthYoutube()

    # GetChannelInfoFromChannelId(youtube, "UCSkLRGGIGKOtinamhcy_42g")
    # GetVideoListFromChannelId(youtube, "UCSkLRGGIGKOtinamhcy_42g")
    GetChannelInfo(youtube)
    GetVideoList(youtube)
    GetVideoInfo(youtube)