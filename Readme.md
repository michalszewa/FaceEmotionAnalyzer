# Face emotion analyzer!
Hi! I made this project for one of my classes. 
## Main technologies
- **Frontend:** Angular 8 (TypeScript), HTML5, CSS (bootstrap framework)
- **Backend:**  C#, ASP .NET Core 2, EF Core, MS SQL Server, Micorosft Azure Services: Face Api Service

# Screens
After uploading photo you will get analyze with detected faces framed by rectangles. You can click on rectangles to get info about particular face.

Below photo you will get chart that shows you how many people on photo has specific emotion (anger, sadness, surprise etc.)

With publish button you can add your analyzed photo to public database and It can be showed on main page where application shows few latest analysis.

![alt text](https://i.ibb.co/zFmKtrW/github2.png)


# Setup
You have to add appsettings.json file that contains code as showed below and specify your credentials for Microsoft Azure's FaceApi Service.

    {
        "AzureFaceApiConfig": {
            "SubscriptionKey": "-",
            "UriBase": "-"
        }
    } 


## Authors
- Michał Szewczak

