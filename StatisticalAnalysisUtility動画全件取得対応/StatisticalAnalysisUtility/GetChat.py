from youtube_comment_scraper_python import *
import DbUtility

if __name__ == '__main__':
    dbUtility = DbUtility.DbUtility()
    video_ids = dbUtility.SelectAll('v_video_info_update_target', 'video_id')
    for video_id in video_ids:
        youtube.open("https://www.youtube.com/watch?v=" + video_id[0])
        youtube.play_pause_video()
        all_data = []
        for i in range ( 0, 10) :
            response = youtube.video_comments()
            if(len(response['errors'])):
                break
            data = response['body']
            all_data.extend(data)

        for data in all_data:
            values = []
            if('user' in data):
                values.append(dbUtility.ConvertToDbStr(data['user'])) 
            else:
               values.append(dbUtility.ConvertToDbStr("uploder"))
            values.append(dbUtility.ConvertToDbStr(data['Comment']))
            if('UserLink' in data):
                values.append(dbUtility.ConvertToDbStr(data['UserLink']))
            else:
               values.append(dbUtility.ConvertToDbStr("uploder"))
            #values.append(data['Time'])
            values.append(dbUtility.ConvertToDbStr(str(int(data['Likes']))))
            values.append(dbUtility.ConvertToDbStr(video_id[0]))

            dbUtility.Insert('t_comment', ['user', 'comment', 'user_link', 'likes', 'video_id'], values)