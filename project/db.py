from datetime import datetime
from pickle import FALSE
import re
from sqlite3 import Cursor
from xml.dom.minidom import TypeInfo
import MySQLdb
import datetime

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
