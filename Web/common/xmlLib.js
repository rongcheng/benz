
////////////////////////////////////////////////////////////////////////////////////
//
// This Code Is Licensed Under The Xopus Open Source License
// More information: http://xopus.org, info@xopus.org
// Mailing list: http://groups.yahoo.com/group/xopus
//
// This product includes software developed by Q42 BV (http://www.q42.nl/).
// please visit http://www.q42.nl for more information or mail us: xopus@q42.nl
//
////////////////////////////////////////////////////////////////////////////////////

function XMLLib() {}

XMLLib.prototype=
{
  load: function (xmlURI, silent)
  {
    var oldStatus = window.status;
    window.status = "Loading '" + xmlURI + "'";

    var xmlHttp = xopus_globs.objectFactory.createXMLHttpRequest();
    xmlHttp.open("GET", xmlURI, false);
    try
    {
      xmlHttp.send(null);
    }
    catch (e)
    {
      return this._loadError(xmlURI, "connectionError", {uri: xmlURI, text: e.description}, oldStatus, silent);
    }
    
    // Try again if the file doesn't exist
    if (xmlHttp.status == 404)
    {
      return this._loadError(xmlURI, "fileNotFound", {uri: xmlURI}, oldStatus, silent);
    }
    // Some other HTTP error occured.
    else if (xmlHttp.status != 200)
    {
      return this._loadError(xmlURI, "httpError", {uri: xmlURI, status: xmlHttp.status, text: xmlHttp.statusText}, oldStatus, silent);
    }
    // No HTTP errors.
    else
    {
      var resultDOM = xopus_globs.objectFactory.createXMLDOM();
      try
      {
        resultDOM.loadXML(xmlHttp.responseText);
      } 
      catch (e) {} // Error handling below
      
      // See if an error occured.
      if (xopus_globs.errorHandler.hasDOMError(resultDOM))
      {
        var hash = xopus_globs.errorHandler.getDOMError(resultDOM, xmlURI);
        return this._loadError(xmlURI, "couldNotParseXML", hash, oldStatus, silent);
      }
      
      window.status = oldStatus;

      return resultDOM;
    }
  },

  _loadError: function (xmlURI, id, hash, oldStatus, silent)
  {
    window.status = oldStatus;
    if (silent) 
      return null;
    else 
    {
      xopus_globs.errorHandler.showRecoverableError(id, hash);

      // Retry loading
      return this.load(xmlURI);
    }
  },

  transformToXml: function (node, xsl, params)
  {
    var proc = xopus_globs.objectFactory.createXSLTProcessor(xsl);
    var outDoc = xopus_globs.objectFactory.createXMLDOM();
    for (var param in params)
      proc.addParameter(param, params[param]);
    proc.input = node;
    proc.output = outDoc;
    var result = proc.transform();
    
//TODO!!!!
//    //see if the transformation succeeded
//    if (outDoc.xml == "") {
//      //probable cause: multi-rooted document
//      //solution: show a warning and wrap the results in a single element
//
//      outDoc = '<div>' + node.transformNode(xsl) + '</div>';
//__xopus_alert(outDoc);
//      outDoc = xopus_globs.objectFactory.createXMLDOM(outDoc);
//__xopus_alert(result + '\n\n' + outDoc.xml);
//    }

    return outDoc;
    
    /*
    var newDOM = xopus_globs.objectFactory.createXMLDOM();
    newDOM.validateOnParse = false;
    node.transformNodeToObject(xsl, newDOM);
    return newDOM;
    */
  },
  
  /**
   * Transform the given xml and produce an error if needed.
   */
  transform: function (xml, transformer, params)
  {
    try
    {
      return this.transformToXml(xml, transformer.xsl, params);
    }
    catch (e)
    {
      xopus_globs.utils.debug(transformer.xsl.xml);
      xopus_globs.errorHandler.showFatalError("xslError", {uri: transformer.uri, reason: e.message || e.description });
    }
  }
};

numScriptsLoading--;

