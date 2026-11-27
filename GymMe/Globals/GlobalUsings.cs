


global using Microsoft.EntityFrameworkCore;
global using FluentValidation;
global using FluentResults;
global using System;
global using System.Text;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.OutputCaching; 
global using GymMe.Data;
global using GymMe.Model;
global using GymMe.Extensions;
global using GymMe.Helpers;
global using GymMe.RateLimiting;
global using GymMe;
global using Microsoft.OpenApi;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.Extensions.DependencyInjection;
global using System.ComponentModel.DataAnnotations;
global using System.Net;
global using GymMe.GlobalValidator;
global using GymMe.GlobalExceptionHandler;
global using System.Security.Claims;
global using System.Threading.RateLimiting;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.JsonWebTokens;
global using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;



// Namespaces //
global using GymMe.Features.Accounts.Login;
global using GymMe.Features.Accounts.RegisterMember;
global using GymMe.Features.Accounts.RegisterTrainer;
global using GymMe.Features.GymPlans.CreatePlan;
global using GymMe.Features.GymPlans.DeletePlan;
global using GymMe.Features.GymPlans.GetAllPlans;
global using GymMe.Features.GymPlans.GetPlan;
global using GymMe.Features.GymPlans.UpdatePlan;
global using GymMe.Features.Members.CreateMember;
global using GymMe.Features.Members.DeleteMember;
global using GymMe.Features.Members.GetAllMembers;
global using GymMe.Features.Members.GetMember;
global using GymMe.Features.Members.UpdateMember;
global using GymMe.Features.Memberships.CreateMembership;
global using GymMe.Features.Memberships.DeleteMembership;
global using GymMe.Features.Memberships.GetMembership;
global using GymMe.Features.Memberships.UpdateMembership;
global using GymMe.Features.GymPlans.MigrateAndDeletePlan;
global using GymMe.Features.Accounts.RegisterAndCreateMember;