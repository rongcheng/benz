
var request = false;

function createRequest() 
{
    try 
    {
        request = new XMLHttpRequest();
    } 
    catch (trymicrosoft) 
    {
        try 
        {
            request = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (othermicrosoft)
        {
            try
            {
                request = new ActiveXObject("Microsoft.XMLHTTP");
            }
            catch (failed)
            {
                request = false;
            }
        }
    }
}

function openRequest(url)
{
    var img =new Image();
    img.src=url;
}

function writeLog(strUser,strIP,strAction,strObject)
{
    var url = "http://125.208.22.26:3333/LogServer.aspx?WebSite=Quanjing&User=" + escape(strUser) + "&IP=" + escape(strIP) + "&Action=" + escape(strAction) + "&Object=" + escape(strObject);
    //createRequest();
    openRequest(url);
}
