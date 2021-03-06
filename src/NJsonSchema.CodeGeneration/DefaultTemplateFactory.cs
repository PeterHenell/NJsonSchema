﻿//-----------------------------------------------------------------------
// <copyright file="DefaultTemplateFactory.cs" company="NJsonSchema">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/rsuter/NJsonSchema/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System;
using System.Reflection;

namespace NJsonSchema.CodeGeneration
{
    /// <summary>The default template factory which loads templates from embedded resources.</summary>
    public class DefaultTemplateFactory : ITemplateFactory
    {
        /// <summary>Creates a template for the given language, template name and template model.</summary>
        /// <remarks>Supports NJsonSchema and NSwag embedded templates.</remarks>
        /// <param name="package">The package name (i.e. language).</param>
        /// <param name="template">The template name.</param>
        /// <param name="model">The template model.</param>
        /// <returns>The template.</returns>
        /// <exception cref="InvalidOperationException">Could not load template..</exception>
        public virtual ITemplate CreateTemplate(string package, string template, object model)
        {
            var typeName = "NSwag.CodeGeneration.CodeGenerators." + package + ".Templates." + template + "Template";
            var type = model.GetType().GetTypeInfo().Assembly.GetType(typeName);
            if (type != null)
                return (ITemplate)Activator.CreateInstance(type, model);

            typeName = "NJsonSchema.CodeGeneration." + package + ".Templates." + template + "Template";
            type = Type.GetType(typeName);
            if (type != null)
                return (ITemplate)Activator.CreateInstance(Type.GetType(typeName, true), model);

            throw new InvalidOperationException("Could not load template '" + template + "'.");
        }
    }
}