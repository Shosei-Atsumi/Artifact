# coding: utf-8
import sys
import codecs
import json
#import urllib.robotparser
from urllib.parse import urlparse
import requests
from requests.exceptions import Timeout
from bs4 import BeautifulSoup
import lxml
import re

class scraping():
    pass

    def check_robot_txt(self, url):
        #rp = urllib.robotparser.RobotFileParser()
        #parsed_url = urlparse(url)
        #domain = parsed_url.scheme + "://"  + parsed_url.netloc
        #rp.set_url(domain + "/robots.txt")
        #rp.read()
        #return rp.can_fetch('*', url)
        return True;

    def start_scraping(self, setting_data):
        # 設定データ取り出し
        c = 0
        settingList = setting_data.split(",")
        for i in settingList:
            settingList[c] = settingList[c].replace(",", "")
            settingList[c] = settingList[c].replace(" ", "")
            c = c + 1

        scrapingUrl = settingList[0]
        tag = settingList[1]
        selectorType = settingList[2]
        selector = settingList[3]
        searchWord = settingList[4]
        deleateWord = settingList[5]
        isDeleateSpace = settingList[6]
        isGetALink = settingList[7]
        getALink = settingList[8]

        # URLが入力されているか
        if(scrapingUrl == "" or scrapingUrl is None):
            print("invalid url")
            return 0

        # 適切なURLかチェック
        if("http" not in scrapingUrl):
            print("http not in url")
            return 0

        # スクレイピング可能なサイトかチェック
        if(self.check_robot_txt(scrapingUrl) == False):
            print("deny scraping")
            #return 0

        # 接続
        try:
            r = requests.get(scrapingUrl, timeout=(2.0, 4.0))
            r.encoding = r.apparent_encoding
        except Timeout:
            return 0

        # 解析
        soup = BeautifulSoup(r.text, 'lxml')
        # タグが設定されている場合
        if(tag != ""):
            # セレクターが設定されている場合
            if(selector != ""):
                if(selectorType == "0"):
                    elems = soup.find_all(tag, id=selector)
                if(selectorType == "1"):
                    elems = soup.find_all(tag, class_=selector)
            else:
                elems = soup.find_all(tag)
        # タグが設定されていない場合
        else:
            # セレクターが設定されている場合
            if(selector != ""):
                if(selectorType == "0"):
                    elems = soup.find_all(id=selector)
                if(selectorType == "1"):
                    elems = soup.find_all(class_=selector)
            else:
                err = "タグ、セレクターどちらも空白"
                print(err)
                return 0
        # 取得したリンクが相対パスだった場合の追加用(スキーマ+ドメイン)
        parsed_url = urlparse(scrapingUrl)
        domain = parsed_url.scheme + "://"  + parsed_url.netloc
        count = 0
        for item in elems:
            title = ""
            ar = ""
            if(len(item.contents) > 0):
                title = item.contents[0].string

            # 特定のワードを指定されている場合、含まれていなければ次のループへ
            if(searchWord != ""):
                if(searchWord not in title):
                    continue

            # 指定要素(正規表現可)を削除
            if(deleateWord != ""):
                    title = re.sub(deleateWord ,"", title)
            # 空白を削除
            if(isDeleateSpace == "1"):
                title = title.replace(" ", "")
            # リンクを取得
            if(isGetALink == "1"):
                if(getALink == "0"):
                    r = item.find_previous('a')
                if(getALink  == "1"):
                    r = item.find('a')
                if(getALink  == "1"):
                    r = item.find_next('a')
                ar = r.get('href')

                # 取得したリンクが相対パスだった場合http～を追加
                if (domain not in ar):
                    ar = domain + ar
            # DBpost用変数に追加
            res[count] = {
                'title': title,
                'link': ar,
                }
            count = count + 1
        return 0

set = list()
for item in sys.argv:
    set.append(item)
res = {}
scraping().start_scraping(set[1])
js = json.dumps(res, ensure_ascii=False)
print(js)