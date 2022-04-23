echo off
title Sign In to AltspaceVR
curl -v -d "user[email]=vn.gomez@uniandes.edu.co&user[password]=Carpe.diem123" https://account.altvr.com/users/sign_in.json -c cookie
