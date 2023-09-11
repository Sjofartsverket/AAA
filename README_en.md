# Background
AAA is a project aimed to develop APIs to share arrival and departure information from ship reports sent to MSW, and pilotage information from the Swedish Maritime Administration's (SMA) pilot planning.

# Authentication and authorization
To be able to call SMA's API:s the clients need a valid `JWT-token`. `JWT` stands for *"JSON Web Tokens"* which is a web standard ([RFC 7519](https://tools.ietf.org/html/rfc7519)) for secure transmission of data between two parties.

The client fetches a valid `JWT-token` from SMA's `IDP`(Identity Provider) and authenticates itself using `Basic authentication scheme` using the paramaters `ClientID` (as username) and `ClientSecret` (as password). The client gets these credentials from SMA through a secure channel (currently email + SMS). `ClientID` is a public identifier of the client. `ClientSecret` is a secret and for the client unique key. 

While fetching a valid `JWT-token`, the client also needs to include the parameter `scopes` (*claims*) containing a list of resources that it intends to consume. Below are the `scopes` that SMA requires for it's public API:s:

- `https://snt-public-api.tst.sjofartsverket.se/` - Basic access to API:s, required  for all resources.
- `https://snt-public-api.tst.sjofartsverket.se/pilotage ` - Required for access to Pilotage APIs.
- `https://snt-public-api.tst.sjofartsverket.se/visit ` - Required for access to Visit APIs.

> Point 1. below shows an example of how a call to fetch a token from SMA's `IDP` can look like.

## Workflow
Below is a picture showing the workflof from the harbours client perspective:

1. The client calls SMA's `IDP` with a `HTTP POST` containing `ClientID` and `ClientSecret`. SMA responds with a *time limited*, signed `JWT-token`. Currently the token is valid for 5 minutes. Below is an example:

  ```
   curl -X POST 'https://idp.tst.sjofartsverket.se/realms/z4_internal/protocol/openid-connect/token' --user $CLIENT_ID:$CLIENT_SECRET -d 'grant_type=client_credentials' -d scope='https://snt-public-api.tst.sjofartsverket.se/ https://snt-public-api.tst.sjofartsverket.se/pilotage https://snt-public-api.tst.sjofartsverket.se/visit'
  ```

> **Note!** `CLIENT_ID` and `CLIENT_SECRET` are pre-set environment variables.

2. The client is now able to call SMA's REST API:s for visit and pilotage information. To be authenticated (and later authorized), the `JWT-token` needs to be set in the HTTP header according to the `Bearer` schema. 

    ```
    Authorization: Bearer <token>
    ```
3. If the client is authenticated and authorized, the API will respond `HTTP 200 OK` and the response body will contain the requested data. `HTTP 401 Unauthorized` indicates that the *authentication* failed, i.e. the `JWT-token` is invalid. The response code `HTTP 403 Forbidden` indicates that the client is not *authorized* to fetch the requested data from the API:s.

![AAA-client_credential-Api-anrop klient.png](images/AAA-client_credential-API-anrop.png)

### More info about JWT tokens
https://jwt.io/introduction

# Links
## Test environment
* Token service: https://idp.tst.sjofartsverket.se/realms/z4_internal/protocol/openid-connect
* API-documentation (in swedish): https://snt-public-api.tst.sjofartsverket.se/swagger/