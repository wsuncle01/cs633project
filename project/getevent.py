import re
import urllib.request
from bs4 import BeautifulSoup
from selenium import webdriver
from lxml import etree
from lxml.html import fromstring, tostring
import db
from progressbar import ProgressBar
import pandas as pd

start=input("start:")#like: 20221204
end=input("end:")

dt1 = pd.date_range(start=start, end=end, freq="D")  
dt=dt1.strftime("%Y%m%d").tolist() # <class 'pandas.core.indexes.datetimes.DatetimeIndex'>


browser = webdriver.Chrome(executable_path='C:\\chromedriver\\chromedriver.exe')
url='https://www.bu.edu/parentsprogram/events/calendar/'
for date in dt:
    a = urllib.request.urlopen(url+date)#打开网址
    html = a.read().decode('utf-8')#读取源代码并转为unicode
    reg=re.compile(r'<div id="events">(.*?)</div>',re.S)
    events=re.findall(reg,html)
    reg=re.compile(r'href="https://(.*?)"',re.S)
    events_url=re.findall(reg,events[0])

    progress = ProgressBar(len(events_url), fmt=ProgressBar.FULL)
    for x in events_url:
        browser.get('https://'+x)
        detail_html = browser.page_source
        selector = etree.HTML(detail_html)
        content = selector.xpath('//body/div/div/div/article')[0]
        original_html = tostring(content)
        soup = BeautifulSoup(original_html, features="html.parser")

        # kill all script and style elements
        for script in soup(["script", "style"]):
            script.extract()    # rip it out


        # get text
        text = soup.get_text().split('\n')
        text=list(filter(None, text))
        e=''
        for i in range(len(text)-1):
            e+=text[i+1]
        db.postmessage(username='system',title=text[0],event=e,tags='auto')
        progress.current += 1
        progress()
    progress.done()