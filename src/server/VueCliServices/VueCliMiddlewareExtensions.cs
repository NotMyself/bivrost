using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices;
using System;

namespace Nyami.AspNetCore.VueCliServices
{
    /// <summary>
    /// Extension methods for enabling Vue CLI middleware support.
    /// </summary>
    public static class VueCliMiddlewareExtensions
    {
        /// <summary>
        /// Handles requests by passing them through to an instance of the Vue CLI server.
        /// This means you can always serve up-to-date CLI-built resources without having
        /// to run the Vue CLI server manually.
        ///
        /// This feature should only be used in development. For production deployments, be
        /// sure not to enable the Vue CLI server.
        /// </summary>
        /// <param name="spaBuilder">The <see cref="ISpaBuilder"/>.</param>
        /// <param name="npmScript">The name of the script in your package.json file that launches the Vue CLI process.</param>
        public static void UseVueCliServer(
            this ISpaBuilder spaBuilder,
            string npmScript)
        {
            if (spaBuilder == null)
            {
                throw new ArgumentNullException(nameof(spaBuilder));
            }

            var spaOptions = spaBuilder.Options;

            if (string.IsNullOrEmpty(spaOptions.SourcePath))
            {
                throw new InvalidOperationException($"To use {nameof(UseVueCliServer)}, you must supply a non-empty value for the {nameof(SpaOptions.SourcePath)} property of {nameof(SpaOptions)} when calling {nameof(SpaApplicationBuilderExtensions.UseSpa)}.");
            }

            VueCliMiddleware.Attach(spaBuilder, npmScript);
        }
    }
}
