<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <title>@ViewBag.Title</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
</head>
<body>
    <div style="width:auto; background-color:#728ea7;">
        @If (Request.IsAuthenticated) Then
            <strong>@Html.Encode(User.Identity.Name) </strong>
            @Html.ActionLink("Log Out", "LogOut", "User")
        Else
            @Html.ActionLink("Register", "Registration", "User")
            <span>|</span>
            @Html.ActionLink("Log In", "logIn", "User")
        End If
    </div>

            @RenderBody()
            @Scripts.Render("~/bundles/jquery")
            @RenderSection("scripts", required: false)
</body>
</html>