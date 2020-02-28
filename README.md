# BlazorForum
BlazorForum is an open source forum application that's built in <a href="https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-3.1" target="_blank">Blazor</a>. BlazorForum has been put together to begin to show some of the cool functionality available in Blazor and hopefully it will gain support and involvement from the C# developer community and grow. Please download BlazorForum, try it out, and contribute to the project. There hasn't been a release created yet, since there's still some work to do.

## TODOs
Some TODOs can be found [Here](TODOS-ISSUES.md)

## A Few Features
* To make setup a little easier, two roles will automatically be added when the first user registers (Administrator & Contributor).
* Since it makes sense the first registered user would be a site owner, the first registered user is automatically assigned to the Administrator role. This makes it easier to gain access to the administration area, without needing to manually add entries into the database. All other newly registered users will be automatically assigned to the Contributor role.
* You can add a customized theme (styles) by adding a new theme folder to the wwwroot/custom-themes/ folder, then selecting it on the settings page in the administration area. The theme name (mentioned it the top of the styles.css file) should match the folder name. For additional requirements, read the [guidelines for adding themes](BlazorForum/wwwroot/custom-themes/ReadMe.txt). A very basic theme called 'Red Theme' has been added to show you how a theme should be structured.
* BlazorForum is using CKEditor, which gives the ability to stylize topics/posts and insert code blocks.

## Database Setup Notes
* To create the database for this project, open it in Visual Studio 2019 and set the **BlazorForum.Data** project as the default project in **Package Manager Console**. Then enter `Update-Database` after **PM>**  This should create and setup your database for the project.

## Join In and Give Feedback
BlazorForum is a project started by me, Steve Elliott, and it's something I've been working on in my free time. I have a full-time job that takes up most of my available time, so I ask that other Blazor/C# developers join in to help make BlazorForum better! Feel free to contact me on my personal website at www.elliottbrand.com.
