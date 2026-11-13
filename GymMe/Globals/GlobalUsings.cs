


global using Microsoft.EntityFrameworkCore;
global using FluentValidation;
global using FluentResults;
global using System;
global using System.Text;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Identity;
global using GymMe.Data;
global using GymMe.Model;
global using GymMe.Extensions;
global using GymMe;
global using Microsoft.OpenApi;

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
global using GymMe.Features.Memberships.GetMembership;