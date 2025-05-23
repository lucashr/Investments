﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Investments.Tests
{
	public class DbSchemaAwareModelCacheKeyFactory : IModelCacheKeyFactory
	{
		/// <inheritdoc />
		public object Create(DbContext context)
		{
			return new { Type = context.GetType(), Schema = context is IDbContextSchema schema ? schema.Schema : null };
		}
	}
}
