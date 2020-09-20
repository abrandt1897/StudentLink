package com.example.studentlink.ui.login;


public class CanvasLogin {

    public static void main(String[] args) {
//        ConnectionClass cc = new ConnectionClass();
//        // create cookie manager to accept all cookies
//        CookieManager CM = new CookieManager();
//        CM.setCookiePolicy(CookiePolicy.ACCEPT_ALL);
//
//        cc.Other_getRequest(URI.create("https://iastate.okta.com/api/v1/authn"), getContext());
//
//        // client connects to the server
//        HttpClient client = HttpClient.newBuilder()
//                .followRedirects(HttpClient.Redirect.ALWAYS)
//                .cookieHandler(CM)
//                .build();
//        // establishing the data to send to okta
//        HttpRequest oktaAuth = HttpRequest.newBuilder()
//                .headers("accept","application/json","x-okta-user-agent-extended","okta-signin-widget-4.1.2","User-Agent","Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36","content-type","application/json")
//                .uri(URI.create("https://iastate.okta.com/api/v1/authn"))
//                .POST(HttpRequest.BodyPublishers.ofString("{\"password\":\"wolf2Link\",\"username\":\"gematera@iastate.edu\",\"options\":{\"warnBeforePasswordExpired\":true,\"multiOptionalFactorEnroll\":true}}"))
//                .build();
//        // initialize the string response
//        HttpResponse<String> response = null;
//        try {
//            // sending the data thru the client, telling it to look in the body for the string
//            response = client.send(oktaAuth, HttpResponse.BodyHandlers.ofString());
//            System.out.println(response.body());
//        } catch (Exception e) {
//            // Auto-generated catch block
//            e.printStackTrace();}
//        // turning string to JSONObject
//        JSONObject obj = new JSONObject(response.body());
//        // grab sessionToken that okta gives
//        String sessionToken = obj.get("sessionToken").toString();
//        System.out.println(sessionToken);
//        // create a uri that sent into okta to login
//        String uri = "https://iastate.okta.com/login/sessionCookieRedirect?checkAccountSetupComplete=true&token="
//                + sessionToken + "&redirectUrl=https%3A%2F%2Fiastate.okta.com%2Fapp%2Fiowastateuniversity_canvas_1%2Fexk14tg9e2nAKZDJ62p7%2Fsso%2Fsaml%3FSAMLRequest%3DfZJfT8IwFMXf%252FRRL37euw%252FCnARKUGFFUIuiDL6R2F2jY2tnbgn57u02iPujr7T3n13NyhyjKouIT73b6Ed48oIvey0Ijbx5GxFvNjUCFXIsSkDvJl5O7Oc%252BSlFfWOCNNQX5I%252FlcIRLBOGU2i2XRE1pBL9sqkiKXspvG5ZP14IDudONv0c2CQ9nqSkegZLAbNiASLIET0MNPohHZhlGZpnA5i1lmxPu90eZa%252BkGgacigtXKPaOVchp1SJoHGQmL0TiTQlFVVFlTm2Y6%252FVoea4j7UU%252BiBwzSi879m52w4g05Pbl%252BlNN6t6FNHQOimJJqc0l0ajL8EuwR6UhKfH%252BTe1NUtOcMg9LcxW6S%252BPxVeHF0rnSm%252F%252Fr%252B%252B1XUJ%252BvVot4sXDckXGw9qHN6XYcQ2tk55CJSr0ZL103kITuV7OhvSnZtiewH2gzaYLUyj5EV0ZWwr392dYwpqJyuNNs8q9xgqk2ijIQzFFYY6XFsIPRiTwgdBxC%252F19auOzTw%253D%253D";
//        HttpRequest oktaLogin = HttpRequest.newBuilder()
//                .headers("Upgrade-Insecure-Requests","1","User-Agent","Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36","Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9")
//                .uri(URI.create(uri))
//                .GET() // get us logged in using the session token
//                .build();
//        try {
//            // sending the login request
//            response = client.send(oktaLogin, HttpResponse.BodyHandlers.ofString());
//        } catch (Exception e) {
//            e.printStackTrace();}
//        // taking string body
//        String responseBody = response.body();
//        // getting the SAMLResponse
//        String SAMLResponse = responseBody.substring(responseBody.indexOf("SAMLResponse\" type=\"hidden\" value=\"")+35, responseBody.indexOf("\"/>\n    <input name=\"RelayState\""));
//        // decoding the symbols
//        String SAMLResponseDecoded = SAMLResponse.replaceAll("&#x2b;", "%2b").replaceAll("&#x3d;", "%3d");
//        String postBody = "SAMLResponse=" + SAMLResponseDecoded;
//        // use SAMLReponse to send the login data
//        HttpRequest canvasLogin = HttpRequest.newBuilder()
//                .headers("Upgrade-Insecure-Requests","1","Origin", "https://iastate.okta.com","Content-Type", "application/x-www-form-urlencoded","User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36", "Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9")
//                .uri(URI.create("https://canvas.iastate.edu/login/saml"))
//                .POST(HttpRequest.BodyPublishers.ofString(postBody))
//                .build();
//        try {
//            response = client.send(canvasLogin, HttpResponse.BodyHandlers.ofString());
//        } catch (Exception e) {
//            e.printStackTrace();}
//        System.out.println(response.statusCode());
//        // get the courses
//        HttpRequest courses = HttpRequest.newBuilder()
//                .headers("Upgrade-Insecure-Requests","1","User-Agent","Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36","Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9")
//                .uri(URI.create("https://canvas.iastate.edu/courses"))
//                .GET()
//                .build();
//        try {
//            response = client.send(courses, HttpResponse.BodyHandlers.ofString());
//        } catch (Exception e) {
//            e.printStackTrace();}
//        System.out.println(response.body());

    }


}

