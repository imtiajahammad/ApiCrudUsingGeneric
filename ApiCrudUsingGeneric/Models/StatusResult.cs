using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.Models
{
    public class StatusResult<T>
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public StatusResult()
        {
            Status = ResponseStatus.FAILED;
            Message = String.Empty;
        }
    }
    public enum ResponseStatus
    {
        FAILED = 0
        ,SUCCESS = 1
        ,NOTFOUND = 2
        /*
         indicates that the REST API can’t map the client’s URI to a resource but may be available in the future

         */
        , LOGINSUCCESS = 3
        ,FETCHSUCCESS = 4
        ,CREATED=5 /* a resource is created inside a collection*/
        , NOCONTENT =6
        /*
         The 204 status code is usually sent out in response to a PUT, POST, or DELETE request when 
        the REST API declines to send back any status message or representation in the response message’s body.
         */
        , BADREQUEST =7
         /*when no other error code is appropriate. Errors can be like malformed request sysntaxx,
          * invalid request message parameters, or deceptive request routing. */
        ,UNAUTHORIZED=8
        /* without providing the proper authorization*/
        , FORBIDDEN =9
        /*indicates that the client’s request is formed correctly, but the REST API refuses to honor it
         the user does not have the necessary permissions for the resource
        Authentication will not help, and the request SHOULD NOT be repeated. 
        Unlike a 401 Unauthorized response, authenticating will make no difference.
         */
        , METHODNOTALLOWED = 10 /* The request HTTP method is known by the server but has been disabled 
                                 * and cannot be used for that resource.*/
        , INTERNALSERVERERROR= 11/*A 500 error is never the client’s fault, and therefore, it is reasonable for the client 
                                  * to retry the same request that triggered this response and hope to get a different response. 
                                  * The API response is the generic error message, given 
                                  * when an unexpected condition was encountered and no more specific message is suitable.*/
        , BADGATEWAY= 12/*The server got an invalid response while working as a gateway to 
                         * get the response needed to handle the request.*/
        , SERVICEUNAVAILABLE= 13/*The server is not ready to handle the request.*/
        , GATEWAYTIMEOUT= 14/*The server is acting as a gateway and cannot get a response in time for a request.*/
        , OKAY=15 /*It indicates that the REST API successfully carried out whatever action the client requested*/
        , DELETED=16
        , UPDATED=17
        , EXCEPTION=18
        ,COMMITERROR=19
        ,NULLPARAMETER=20

    }
}
