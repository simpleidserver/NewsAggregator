﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewsAggregator.Core.Resources {
    using System;
    
    
    /// <summary>
    ///   Une classe de ressource fortement typée destinée, entre autres, à la consultation des chaînes localisées.
    /// </summary>
    // Cette classe a été générée automatiquement par la classe StronglyTypedResourceBuilder
    // à l'aide d'un outil, tel que ResGen ou Visual Studio.
    // Pour ajouter ou supprimer un membre, modifiez votre fichier .ResX, puis réexécutez ResGen
    // avec l'option /str ou régénérez votre projet VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Global {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Global() {
        }
        
        /// <summary>
        ///   Retourne l'instance ResourceManager mise en cache utilisée par cette classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NewsAggregator.Core.Resources.Global", typeof(Global).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Remplace la propriété CurrentUICulture du thread actuel pour toutes
        ///   les recherches de ressources à l'aide de cette classe de ressource fortement typée.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Article is already like by the user.
        /// </summary>
        internal static string ArticleAlreadyLikedByTheUser {
            get {
                return ResourceManager.GetString("ArticleAlreadyLikedByTheUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Article is not liked by the user.
        /// </summary>
        internal static string ArticleNotLikedByTheUser {
            get {
                return ResourceManager.GetString("ArticleNotLikedByTheUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Feed cannot be removed by the user {0}.
        /// </summary>
        internal static string CannotRemoveFeed {
            get {
                return ResourceManager.GetString("CannotRemoveFeed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à DataSource {0} already exists.
        /// </summary>
        internal static string DatasourceAlreadyExists {
            get {
                return ResourceManager.GetString("DatasourceAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à DataSource {0} doesn&apos;t exist.
        /// </summary>
        internal static string DataSourceDoesntExist {
            get {
                return ResourceManager.GetString("DataSourceDoesntExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Title is empty.
        /// </summary>
        internal static string TitleIsEmpty {
            get {
                return ResourceManager.GetString("TitleIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à UserId is empty.
        /// </summary>
        internal static string UserIdIsEmpty {
            get {
                return ResourceManager.GetString("UserIdIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à User is not authorized to subscribe.
        /// </summary>
        internal static string UserNotAuthorizedToSubscribe {
            get {
                return ResourceManager.GetString("UserNotAuthorizedToSubscribe", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à User is not authorized to unsubscribe.
        /// </summary>
        internal static string UserNotAuthorizedToUnSubscribe {
            get {
                return ResourceManager.GetString("UserNotAuthorizedToUnSubscribe", resourceCulture);
            }
        }
    }
}
