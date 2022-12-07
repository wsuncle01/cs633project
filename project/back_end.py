from flask import Flask,session
import wtf
import os
from flask import Blueprint, render_template, flash, redirect, url_for, request, current_app
import db
import traceback
import json
import send_mail

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
    return render_template('readme.html')

#register page
@app.route('/account/register',methods=['GET', 'POST'])
def register():
    form = wtf.RegistrationForm()
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
    form=wtf.Loginform()
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
                            if not db.get_user_level(username):
                                session['user_level']='4'
                                return redirect(url_for('user_level'))
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


@app.route('/events/post',methods=['POST','GET'])
def post_event():
    form=wtf.post_events_form()
    if request.method=='POST':
        events=request.form.get('events')
        tags=request.form.get('tags') 
        title=request.form.get('title')
        if form.validate_on_submit():
            name=session.get('username')
            if name:
                db.postmessage(name,title,events,tags)
                flash('Post successfully')
            else:
                flash('Please login first')
        else:
            for i in form.errors:
                flash(form.errors[i],'error')
    return render_template('postevent.html',form=form)

@app.route('/events',methods=['POST','GET'])
def events():
    if request.method=='GET':
        name=session.get('username')
        if name:
            eventsinfo=db.events()
            d={}
            eid=[]
            title=[]
            tags=[]
            tagid=[]
            author=[]
            events=[]
            date=[]
            for i in eventsinfo:# convert turple to dict
                eid.append(i[0])
                title.append(i[1])
                tags.append(i[2])
                tagid.append(i[3])
                author.append(i[4])
                events.append(i[5])
                date.append(str(i[6]))
            d['id']=eid
            d['title']=title
            d['tags']=tags
            d['tagid']=tagid
            d['author']=author
            d['events']=events
            d['date']=date
            return json.dumps(d)
        else:
            return json.dumps({'login_status':False})

@app.route('/account/user_level',methods=['POST','GET'])
def user_level():
    form=wtf.user_level_form()
    level=session.get('user_level')
    name=session.get('username')
    if request.method == "POST":
        if level == '0' or level == '4':
            if level == '4':
                session['user_level']=db.get_user_level(name)
        else:
            return render_template('level_warning.html')
        if form.validate_on_submit():
            if name:
                radio = form.level.data
                db.set_user_level(radio,name)
                return redirect(url_for('start'))
            else:
                flash('Please login first')
        else:
            for i in form.errors:
                flash(form.errors[i],'error')
    return render_template('user_level.html',form=form)


@app.route('/account/confirm_email',methods=["POST","GET"])
def confirm_email():
    mailadress=request.args.get('mail_adress')
    vercode=send_mail.confirm_email(mailadress)
    if vercode=="False":
        d={'status':False}
        return json.dumps(d)
    else:
        session['vercode']=vercode
        d={'status':True,'vercode':vercode}
        return json.dumps(d)

@app.route('/events/search',methods=['POST','GET'])
def search_event():
    q=request.args.get('q')
    results=db.search_events(q=q)
    d={}
    id=[]
    for i in results:
        id.append(i[0])
    d['id']=id
    return json.dumps(d)


if __name__== '__main__':
    app.run()