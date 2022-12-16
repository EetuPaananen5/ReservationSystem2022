namespace ReservationSystem2022.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "ApiKey"; //etsitään hederistä määritettyä avainta APIKEYNAME
        public ApiKeyMiddleware(RequestDelegate next) //Konstruktorin ReDel ottaa vastaan ja kertoo seuraavan käsitteljän

        {
           _next = next;
        }
        public async Task InvokeAsync(HttpContext context) // funktio joka käsittelee saapuvan kutsun, invoke ottaa vastaan.
        {
            if(!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey)) //onko avain vai ei? extractedapikey syötetty avain
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api key is missing");
                return;
            }
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>(); // mikä salasana, hae
            var apiKey = appSettings.GetValue<string>(APIKEYNAME);
            if(!apiKey.Equals(extractedApiKey)) // ei tee mitää jos kaikki ok
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Unauthorised client");
                return ;

            }
            await _next(context);   // kaikki kunnossa ->siirrä eteepäin
        }
    }
}
