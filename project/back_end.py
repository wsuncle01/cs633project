from flask import Flask,session
import wtf
from wtf import RegistrationForm,Loginform
import os
from flask import Blueprint, render_template, flash, redirect, url_for, request, current_app
import db
import traceback
import json

#get workfold path
file=str(__file__)
file=file.split('\\')
path=''
for i in range(len(file)-1):
    path=path+file[i]+'/'

#set fold path
app=Flask(__name__,template_folder='templates',static_folder=path)

#set flask config
app.config['SESSION_TYPE'] = 'filesystem'
app.config['SECRET_KEY'] = os.urandom(24)

#home page: now for introducton
@app.route('/')
def start():
    with open(path+'readme.txt','r') as f: #open introduction file
        data=f.readlines()
        text=''
        for i in data:
            text+='<p>'+i+'</p>'
    return text

#register page
@app.route('/account/register',methods=['GET', 'POST'])
def register():
    form = RegistrationForm()
    if request.method == 'POST':
        #get WTForms imformation
        username=   request.form.get('username')
        email   =   request.form.get('email')
        passwd1 =   request.form.get('passwd1')
        passwd2 =   request.form.get('passwd2')

        if form.validate_on_submit():#verificate or not
            if db.name_exsit_status(username):
                try:
                    db.register(username,passwd1,email)
                    flash('Thanks for registering')
                except:
                    flash('Database failure')
                    traceback.print_exc()
                    print(traceback.format_exc())

                return redirect(url_for('login'))#link to login page

            else:#print why form doesn't verificate
                flash('Username exist')
        else:
            for i in form.errors:
                flash(form.errors[i],'error') 
    return render_template('register.html', form=form)

@app.route('/account/login',methods=['GET','POST'])
def login():
    form=Loginform()
    if request.method == 'POST':
        username    =   request.form.get('username')
        passwd      =   request.form.get('passwd')
        if form.validate_on_submit():#verificate or not
            try:
                l1,l2=db.login_check(username,passwd)#l1 means username exist status; l2 means password right or not
                l3=db.multilogincheck(username)
                if not l1:
                    flash('username cannot find')
                else:
                    if l3:
                        if not l2:
                            flash('password wrong')
                        else:
                            session['username']=username
                            db.login_status(username)
                            flash('login successful')
                    else:
                        flash('Do not multipule login')
            except:
                flash('Database failure')
                traceback.print_exc()
                print(traceback.format_exc())
    return render_template('login.html', form=form)

@app.route('/account/logout',methods=['GET','POST'])
def logout():
    name=session.get('username')
    session['username']=False
    db.logout(name)
    return redirect(url_for('login'))

@app.route('/communication/send_message',methods=['GET','POST'])
def send_message():
    form=wtf.send_message_form()
    if request.method=='POST':
        name=request.form.get('receive_username')
        message=request.form.get('sendbox')
        if form.validate_on_submit():#verificate or not
            if session.get('username'):
                if not db.name_exsit_status(name):
                    db.sendmessage(session.get('username'),name,message)
                    flash('Message has been sent')
                else:
                    flash('Target user does not exist')
            else:
                flash('Please login first')
        else:
            for i in form.errors:
                flash(form.errors[i],'error') 
    return render_template('message_send_page.html',form=form)

@app.route('/communication/mailbox',methods=['POST','GET'])
def messagebox():
    if request.method=='GET':
        name=session.get('username')
        if name:
            message=db.readmessage(name)
            d={}
            mid=[]
            fuser=[]
            tuser=[]
            mes=[]
            dat=[]
            for i in message:# convert turple to dict
                mid.append(i[0])
                fuser.append(i[1])
                tuser.append(i[2])
                mes.append(i[3])
                dat.append(str(i[4]))
            d['id']=mid
            d['from_user']=fuser
            d['to_user']=tuser
            d['message']=mes
            d['date']=dat
            return json.dumps(d)
        else:
            return json.dumps({'login_status':False})
        
if __name__== '__main__':
    app.run()