using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using static System.Collections.Specialized.BitVector32;
using System.Reflection.Emit;
using System;
using TASK_MOCK_MVC.Migrations;

namespace TASK_MOCK_MVC.Entities.Enum;
public enum ERole
{
    ADMIN,
    MANAGER,
    USER
}
/*@model TASK_MOCK_MVC.Entities.TaskModel
@{
    ViewData["Title"] = "Create";
}
@* <head>
    <link rel = "stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
</head> *@
<h1>Create</h1>
@* <div class="row">
    <div class=" col-md-4">
        <form asp-controller="TaskModel"  asp-action="Create" method="post">
            <div asp-validation-summary="ModelOnly" class="text-danger">
                <div class="form-group">
                    <label asp-for="Title" class="control-label"></label>
                    <input asp-for="Title" class="form-control"/>
                    <span asp-validation-for="Title" class="text-danger"></span>
                </div>
                <div class="form-group">
                    <label asp-for="Description" class="control-label"></label>
                    <input asp-for="Description" class="form-control" />
                    <span asp-validation-for="Description" class="text-danger"></span>
                </div>
                <div class="form-group">
                    <label for="Email">Email</label>
                    <input type = "email" id="Email" name="Email" class="form-control"/>
                </div>                
                <div class="form-group">
                    <label asp-for="DueDate" class="control-label"></label>
                    <input asp-for="DueDate" class="form-control" type="date" />
                    <span asp-validation-for="DueDate" class="text-danger"></span>
                </div>                
                <div class="form-group">
                    <label asp-for="Status" class="control-label"></label>
                    <select asp-for="Status" class="form-control">
                        <option value = "" > Select status</option>
                        <option value = "0" > Created </ option >
                        < option value="1">In Progress</option>
                        <option value = "2" > Completed </ option >
                    </ select >
                    < span asp-validation-for="Status" class="text-danger"></span>
                </div>
                <div class="form-group">
                    <input type = "submit" value="Create"  class="btn btn-primary my-2"/>
                </div>
            </div>
        </form>
    </div>
</div> *@
<head>
    <link rel = "stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
</head>
@* <body translate = "no" >
    < form asp-controller="TaskModel" asp-action="Create" method="post" enctype="multipart/form-data" class="col-md-6" style="margin: 5px auto;">
        <div class="login-box">
            <a asp-controller="TaskModel" asp-action="Index"><h3>Back</h3></a>
            <div class="textbox>
                <i class="fa fa-user" aria-hidden="true"></i>
                <input asp-for="Title" id="form1-Admin" placeholder="Title">
            </div>
            <div class="textbox>
                <i class="fa fa-user" aria-hidden="true"></i>
                <input asp-for="Description" id="form2-Admin" placeholder="Description">
            </div>
            <div class="textbox>
                <i class="fa fa-user" aria-hidden="true"></i>
                <input asp-for="DueDate" id="form3-Admin" placeholder="DueDate">
            </div>
            <div class="form-group">
                <label asp-for="Status" class="control-label"></label>
                <select asp-for="Status" class="form-control">
                    <option value = "" > Select status</option>
                    <option value = "0" > Created </ option >
                    < option value="1">In Progress</option>
                    <option value = "2" > Completed </ option >
                </ select >
                < span asp-validation-for="Status" class="text-danger"></span>
            </div>
            <div class="form-group">
                <input type = "submit" value="Create" class="btn btn-primary my-2" />
            </div>
        </div>
    </form>
</body> *@
<div>
    <a asp-action="Index">Back To List</a>
</div>
@section Scripts
{
    @{ await Html.RenderPartialAsync("_ValidationScriptsPartial"); }
}*/
