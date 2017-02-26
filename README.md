# Swedish, Norwegian BankID and Danish NemID in ASP.NET Core with OIDC
Finished sample as described in https://grean.com/easyid/aspnetcore/oidc/2017/02/25/aspnet-core-and-easyid.html.

*Note:* This sample was put together and tried on Mac OSX.

This sample illustrates how to set up an ASP.NET Core application that authenticates
users using *OpenID Connect* with one of the Scandinavian national or bank 
idenntities services. This is done with Grean's easyID as the middelman.

Out of the box this sample asks for authentication using Swedish BankID.
To get a test user go to [https://demo.bankid.com](https://demo.bankid.com).

## Run the sample

1. Clone this repo
2. Inside the cloned repo, run `dotnet restore; dotnet build`
3. To run immediately insert the values for `Authority`, `ClientID`, and `ClientSecret` given
in the comments in the `Startup.cs` file.
4. `dotnet run` will make the site available on [http://localhost:5000](http://localhost:5000).

## Grean easyID
If you need to know the real identity of your users, spend a few minutes to hook
into the various national and bank identity services through Grean easyID. 
Sign up here: [grean.com/easyid](https://grean.com/easyid).