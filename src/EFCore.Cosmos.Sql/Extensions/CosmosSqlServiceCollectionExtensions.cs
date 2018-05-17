// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Cosmos.Sql.Infrastructure;
using Microsoft.EntityFrameworkCore.Cosmos.Sql.Query;
using Microsoft.EntityFrameworkCore.Cosmos.Sql.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CosmosSqlServiceCollectionExtensions
    {
        public static IServiceCollection AddEntityFrameworkCosmosSql([NotNull] this IServiceCollection serviceCollection)
        {
            Check.NotNull(serviceCollection, nameof(serviceCollection));

            var builder = new EntityFrameworkServicesBuilder(serviceCollection)
                .TryAdd<IDatabaseProvider, DatabaseProvider<CosmosSqlDbOptionsExtension>>()
                .TryAdd<IQueryContextFactory, CosmosSqlQueryContextFactory>()
                    .TryAdd<IDatabase, CosmosSqlDatabase>()
                    .TryAdd<IDatabaseCreator, CosmosSqlDatabaseCreator>()
                    .TryAdd<IEntityQueryModelVisitorFactory, CosmosSqlEntityQueryModelVisitorFactory>()
                    .TryAdd<IEntityQueryableExpressionVisitorFactory, CosmosSqlEntityQueryableExpressionVisitorFactory>()
                    .TryAddProviderSpecificServices(
                        b => b
                            .TryAddScoped<CosmosClient, CosmosClient>()

                    );

            builder.TryAddCoreServices();

            return serviceCollection;
        }
    }
}
