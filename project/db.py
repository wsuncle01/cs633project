from ast import keyword
from asyncio.windows_events import NULL
from datetime import datetime
from pickle import FALSE
import re
from select import select
from sqlite3 import Cursor
from turtle import up
from unittest import result
from xml.dom.minidom import TypeInfo
import MySQLdb
import datetime
from collections import defaultdict

dbpassword="mysqlQq1538773813hz"
databasename="cs633project"

def register(name:str,password:str,email:str):
    db=MySQLdb.connect("localhost","root",dbpassword,databasename)
    cursor = db.cursor()
    date=str(datetime.datetime.now()).split(' ')[0]
    sql = "INSERT INTO userinfo(username,password, useremail,submission_date) VALUES ('{0}', '{1}', '{2}','{3}')".format(name,password,email,date)
    cursor.execute(sql)
    db.commit()
    db.close()

def name_exsit_status(name:str):
    db=MySQLdb.connect("localhost","root",dbpassword,databasename)
    cursor = db.cursor()
    search="SELECT username,password FROM userinfo WHERE username='{0}'".format(name)
    cursor.execute(search)
    results = cursor.fetchall()
    if not results:
        return True
    else:
        return False

def login_check(name:str,password:str):
    db=MySQLdb.connect("localhost","root",dbpassword,databasename)
    cursor=db.cursor()
    search="SELECT username,password FROM userinfo WHERE username='{0}'".format(name)
    cursor.execute(search)
    results = cursor.fetchall()
    db.commit()
    db.close()
    if not results:
        return False,False
    for row in results:
        dbpassworb=row[1]
        if dbpassworb == password:
            return True,True
    return True,False

def login_status(name:str):
    db=MySQLdb.connect("localhost","root",dbpassword,databasename)
    cursor=db.cursor()
    search="SELECT username FROM loginstatus WHERE username='{0}'".format(name)
    cursor.execute(search)
    results = cursor.fetchall()
    date=str(datetime.datetime.now())
    if not results:
        date=str(datetime.datetime.now())
        create="INSERT INTO loginstatus(username,status,last_login_date) VALUES ('{0}',{1},'{2}')".format(name,True,date)
        cursor.execute(create)
    else:
        date=str(datetime.datetime.now())
        update="UPDATE loginstatus SET status=True,last_login_date='{0}' WHERE username='{1}'".format(date,name)
        cursor.execute(update)
    db.commit()
    db.close()

def multilogincheck(name:str):
    db=MySQLdb.connect("localhost","root",dbpassword,databasename)
    cursor=db.cursor()
    search="SELECT status FROM loginstatus WHERE username='{0}'".format(name)
    cursor.execute(search)
    results = cursor.fetchall()
    if not results:
        return True
    else:
        if results[0][0]==0:
            return True
        else:
            return False


def logout(name:str):
    db=MySQLdb.connect("localhost","root",dbpassword,databasename)
    cursor=db.cursor()
    update="UPDATE loginstatus SET status=False WHERE username='{0}'".format(name)
    cursor.execute(update)
    db.commit()
    db.close()

def sendmessage(from_user:str,to_user:str,message:str):
    db=MySQLdb.connect("localhost","root",dbpassword,databasename)
    cursor=db.cursor()
    date=str(datetime.datetime.now())
    insert="INSERT INTO messagebox(from_user,to_user,message,date) VALUES ('{0}','{1}','{2}','{3}')".format(from_user,to_user,message,date)
    cursor.execute(insert)
    db.commit()
    db.close()

def readmessage(username:str):
    db=MySQLdb.connect("localhost","root",dbpassword,databasename)
    cursor=db.cursor()
    select="SELECT * FROM messagebox WHERE to_user='{0}'".format(username)
    cursor.execute(select)
    results=cursor.fetchall()
    db.commit()
    db.close()
    return results


def taglistupdate(tags:str):
    db=MySQLdb.connect("localhost","root",dbpassword,databasename)
    cursor=db.cursor()
    select="SELECT * FROM tag"
    cursor.execute(select)
    results=cursor.fetchall()
    exist_tag=defaultdict(int)
    for i in results:
        for j in i[1].split(','):
            exist_tag[j]+=i[2]
    curent_tag=exist_tag.copy()
    taglist=tags.split(',')
    for i in taglist:
        exist_tag[i]+=1
    for i in exist_tag:
        if i in curent_tag:
            update="UPDATE tag SET queto_num={0} WHERE tagname='{1}'".format(exist_tag[i],i)
            cursor.execute(update)
        else:
            insert="INSERT INTO tag(tagname,queto_num) VALUES ('{0}',{1})".format(i,exist_tag[i])
            cursor.execute(insert)
    db.commit()
    db.close()

def get_tagid(name:str):
    db=MySQLdb.connect("localhost","root",dbpassword,databasename)
    cursor=db.cursor()
    select="SELECT tagid FROM tag WHERE tagname='{0}' limit 1".format(name)
    cursor.execute(select)
    result=cursor.fetchall()
    db.commit()
    db.close()
    return result[0][0]

def postmessage(username:str,title:str,event:str,tags:str):
    db=MySQLdb.connect("localhost","root",dbpassword,databasename)
    cursor=db.cursor()
    taglistupdate(tags)
    taglist=tags.split(',')
    tagid=""
    for i in taglist:
        tagid=tagid+str(get_tagid(i))+','
    date=str(datetime.datetime.now())
    insert="INSERT INTO events(title,tags,tagid,author,event,date) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')".format(title,
                                                                                                                        tags,
                                                                                                                        tagid,
                                                                                                                        username,
                                                                                                                        event,
                                                                                                                        date)
    cursor.execute(insert)
    db.commit()
    db.close()

def events():
    db=MySQLdb.connect("localhost","root",dbpassword,databasename)
    cursor=db.cursor()
    select="SELECT * FROM events"
    cursor.execute(select)
    results=cursor.fetchall()
    db.commit()
    db.close()
    return results

def set_user_level(level:str,username:str):
    db=MySQLdb.connect("localhost","root",dbpassword,databasename)
    cursor=db.cursor()
    select="SELECT id,status FROM userlevel WHERE username='{0}' limit 1".format(username)
    cursor.execute(select)
    results=cursor.fetchall()
    if not results:
        insert="INSERT INTO userlevel(username,status) VALUES ('{0}','{1}')".format(username,level)
        cursor.execute(insert)
    else:
        update="UPDATE userlevel SET status='{0}' WHERE username='{1}'".format(level,username)
        cursor.execute(update)
    db.commit()
    db.close()

def get_user_level(username:str):
    db=MySQLdb.connect("localhost","root",dbpassword,databasename)
    cursor=db.cursor()
    select="SELECT status FROM userlevel WHERE username='{0}' limit 1".format(username)
    cursor.execute(select)
    results=cursor.fetchall()
    db.commit()
    db.close()
    if results:
        return results[0][0]
    else:
        return NULL

def search_events(q:str):
    db=MySQLdb.connect("localhost","root",dbpassword,databasename)
    cursor=db.cursor()
    keywords=q.split(" ")
    print(keywords[0])
    select="SELECT id FROM events WHERE title like '%{0}%' or tags like '%{0}%' or event like '%{0}%' ".format(keywords[0])
    i=1
    while i<len(keywords):
        select=select+"or title like '%{0}%' or tags like '%{0}%' or event like '%{0}%'".format(keywords[i])
        i+=1
    cursor.execute(select)
    results=cursor.fetchall()
    db.commit()
    db.close()
    return results
