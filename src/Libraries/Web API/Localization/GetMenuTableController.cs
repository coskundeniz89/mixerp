using MixERP.Net.Framework;
using MixERP.Net.Entities.Localization;
using MixERP.Net.Schemas.Localization.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using PetaPoco;
using MixERP.Net.EntityParser;
namespace MixERP.Net.Api.Localization
{
    /// <summary>
    /// Provides a direct HTTP access to execute the function GetMenuTable.
    /// </summary>
    [RoutePrefix("api/v1.5/localization/procedures/get-menu-table")]
    public class GetMenuTableController : ApiController
    {
        /// <summary>
        /// Login id of application user accessing this API.
        /// </summary>
        public long _LoginId { get; set; }

        /// <summary>
        /// User id of application user accessing this API.
        /// </summary>
        public int _UserId { get; set; }

        /// <summary>
        /// Currently logged in office id of application user accessing this API.
        /// </summary>
        public int _OfficeId { get; set; }

        /// <summary>
        /// The name of the database where queries are being executed on.
        /// </summary>
        public string _Catalog { get; set; }

        private GetMenuTableProcedure procedure;
        public class Annotation
        {
            public string CultureCode { get; set; }
        }

        public GetMenuTableController()
        {
            this._LoginId = AppUsers.GetCurrent().View.LoginId.ToLong();
            this._UserId = AppUsers.GetCurrent().View.UserId.ToInt();
            this._OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
            this._Catalog = AppUsers.GetCurrentUserDB();
            this.procedure = new GetMenuTableProcedure
            {
                _Catalog = this._Catalog,
                _LoginId = this._LoginId,
                _UserId = this._UserId
            };
        }
        /// <summary>
        ///     Creates meta information of "get menu table" annotation.
        /// </summary>
        /// <returns>Returns the "get menu table" annotation meta information to perform CRUD operation.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("annotation")]
        [Route("~/api/localization/procedures/get-menu-table/annotation")]
        public EntityView GetAnnotation()
        {
            return new EntityView
            {
                Columns = new List<EntityColumn>()
                                {
                                        new EntityColumn { ColumnName = "_culture_code",  PropertyName = "CultureCode",  DataType = "string",  DbDataType = "text",  IsNullable = false,  IsPrimaryKey = false,  IsSerial = false,  Value = "",  MaxLength = 0 }
                                }
            };
        }

        /// <summary>
        ///     Creates meta information of "get menu table" entity.
        /// </summary>
        /// <returns>Returns the "get menu table" meta information to perform CRUD operation.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("meta")]
        [Route("~/api/localization/procedures/get-menu-table/meta")]
        public EntityView GetEntityView()
        {
            return new EntityView
            {
                Columns = new List<EntityColumn>()
                                {
                                        new EntityColumn { ColumnName = "menu_code",  PropertyName = "MenuCode",  DataType = "string",  DbDataType = "text",  IsNullable = false,  IsPrimaryKey = false,  IsSerial = false,  Value = "",  MaxLength = 0 },
                                        new EntityColumn { ColumnName = "invariant",  PropertyName = "Invariant",  DataType = "string",  DbDataType = "text",  IsNullable = false,  IsPrimaryKey = false,  IsSerial = false,  Value = "",  MaxLength = 0 },
                                        new EntityColumn { ColumnName = "localized",  PropertyName = "Localized",  DataType = "string",  DbDataType = "text",  IsNullable = false,  IsPrimaryKey = false,  IsSerial = false,  Value = "",  MaxLength = 0 }
                                }
            };
        }

        [AcceptVerbs("POST")]
        [Route("execute")]
        [Route("~/api/localization/procedures/get-menu-table/execute")]
        public IEnumerable<DbGetMenuTableResult> Execute([FromBody] Annotation annotation)
        {
            try
            {
                this.procedure.CultureCode = annotation.CultureCode;


                return this.procedure.Execute();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch (MixERPException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    Content = new StringContent(ex.Message),
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }
    }
}