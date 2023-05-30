# FitBitAuthenticator 1.4

![image](/Images/logo.png)

This is special fork of [FitBit_Xamarin_Forms](https://github.com/vipinjohney/FitBit_Xamarin_Forms) project.
It's using model: FitBit auth to get fresh user acess token.

## About 
Implementing FitBit login and user api's in Xamarin Forms application.

## Screenshots

![image](/Images/shot1.png)

![image](/Images/shot2.png)


## My 2 cents
- UWP "target" added/"injected" + Xam.Essentials ported to .NET Standard 1.4. 
- Min. os. win. switched to 15063. .NET Standard 1.4 used.

## Status
- Target iOS: not tested.
- Target Android: ok
- Target UWP: the login page (at popup window) can't display... so strange... I confused :(

## Tech. details
You can find the steps by step explanation [here](https://medium.com/@vipin.johney/fitbit-authentication-xamarin-forms-5900ed8e9caa?source=friends_link&sk=bd8ec5376d3c6c32e02a4d1524679732).

## Hot to get FitBit credentials?
- Follow https://dev.fitbit.com
- Register new app (I choosed Client type of app... however, Server recommended)

## TODO
- Try to use "silent auth flow mode" (change App type to "Personal" and do some code review...)
- Try to find some "FitBit substitution" (it's needed for "server part" of my uncompleted MSB project)

## Credits 
[Vipin Johney](https://github.com/vipinjohney/) iOS Application Developer, wanderlust, technology enthusiast.

## ..
AS IS. No support. RnD only. DIY.

## .
[m][e] 2023