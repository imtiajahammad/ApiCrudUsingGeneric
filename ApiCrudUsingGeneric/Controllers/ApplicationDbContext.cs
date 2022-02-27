﻿using ApiCrudUsingGeneric.Models;
using Microsoft.EntityFrameworkCore;
using System;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}

	public DbSet<Student> Students { get; set; }
	public DbSet<Teacher> Teachers { get; set; }
}