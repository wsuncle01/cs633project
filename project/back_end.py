from flask import Flask
from wtf import RegistrationForm,Loginform
import os
from flask import Blueprint, render_template, flash, redirect, url_for, request, current_app
import db
import traceback

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
                if not l1:
                    flash('username cannot find')
                else:
                    if not l2:
                        flash('password wrong')
                    else:
                        db.login_status(username)
                        flash('login successful')
            except:
                flash('Database failure')
                traceback.print_exc()
                print(traceback.format_exc())
    return render_template('login.html', form=form)
    


if __name__== '__main__':
    app.run()