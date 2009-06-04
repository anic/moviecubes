<%--
  Licensed to the Apache Software Foundation (ASF) under one or more
  contributor license agreements.  See the NOTICE file distributed with
  this work for additional information regarding copyright ownership.
  The ASF licenses this file to You under the Apache License, Version 2.0
  (the "License"); you may not use this file except in compliance with
  the License.  You may obtain a copy of the License at
  
  http://www.apache.org/licenses/LICENSE-2.0
  
  Unless required by applicable law or agreed to in writing, software
  distributed under the License is distributed on an "AS IS" BASIS,
  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  See the License for the specific language governing permissions and
  limitations under the License.
--%>
<%@ page 
  session="false"

  import="java.io.*"
  import="java.util.*"
  import="java.net.*"
  import="javax.servlet.http.*"
  import="javax.servlet.*"
  import="sun.misc.BASE64Encoder"
  import="sun.misc.BASE64Decoder"
  import="org.apache.nutch.html.Entities"
  import="org.apache.nutch.metadata.Nutch"
  import="org.apache.nutch.searcher.*"
  import="org.apache.nutch.plugin.*"
  import="org.apache.nutch.clustering.*"
  import="org.apache.hadoop.conf.*"
  import="org.apache.nutch.util.NutchConfiguration"
 
%>
<Root>
<%!
  /**
   * Number of hits to retrieve and cluster if clustering extension is available
   * and clustering is on. By default, 100. Configurable via nutch-conf.xml.
   */
  private int HITS_TO_CLUSTER;

  /**
   * Maximum hits per page to be displayed.
   */
  private int MAX_HITS_PER_PAGE;

  /**
   * An instance of the clustering extension, if available.
   */
  private OnlineClusterer clusterer;
  
  /**
   * Nutch configuration for this servlet.
   */
  private Configuration nutchConf;

  /**
   * Initialize search bean.
   */
  public void jspInit() {
    super.jspInit();
    
    final ServletContext application = getServletContext(); 
    nutchConf = NutchConfiguration.get(application);
	  HITS_TO_CLUSTER = nutchConf.getInt("extension.clustering.hits-to-cluster", 100);
    MAX_HITS_PER_PAGE = nutchConf.getInt("searcher.max.hits.per.page", -1);

    try {
      clusterer = new OnlineClustererFactory(nutchConf).getOnlineClusterer();
    } catch (PluginRuntimeException e) {
      super.log("Could not initialize online clusterer: " + e.toString());
    }
  }
%>

<%
  // The Nutch bean instance is initialized through a ServletContextListener 
  // that is setup in the web.xml file
  NutchBean bean = NutchBean.get(application, nutchConf);
  // set the character encoding to use when interpreting request values 
  request.setCharacterEncoding("UTF-8");

  bean.LOG.info("query request from " + request.getRemoteAddr());

  // get query from request
  String queryString = request.getParameter("query");
  if (queryString == null)
    queryString = "";
  String htmlQueryString = Entities.encode(queryString);
  
  // a flag to make the code cleaner a bit.
  boolean clusteringAvailable = (clusterer != null);

  String clustering = "";
  if (clusteringAvailable && "yes".equals(request.getParameter("clustering")))
    clustering = "yes";

  int start = 0;          // first hit to display
  String startString = request.getParameter("start");
  if (startString != null)
    start = Integer.parseInt(startString);

  int hitsPerPage = 10;          // number of hits to display
  String hitsString = request.getParameter("hitsPerPage");
  if (hitsString != null)
    hitsPerPage = Integer.parseInt(hitsString);
  if(MAX_HITS_PER_PAGE > 0 && hitsPerPage > MAX_HITS_PER_PAGE)
    hitsPerPage = MAX_HITS_PER_PAGE;

  int hitsPerSite = 2;                            // max hits per site
  String hitsPerSiteString = request.getParameter("hitsPerSite");
  if (hitsPerSiteString != null)
    hitsPerSite = Integer.parseInt(hitsPerSiteString);

  String sort = request.getParameter("sort");
  boolean reverse =
    sort!=null && "true".equals(request.getParameter("reverse"));

  String params = "&hitsPerPage="+hitsPerPage
     +(sort==null ? "" : "&sort="+sort+(reverse?"&reverse=true":""));

  int hitsToCluster = HITS_TO_CLUSTER;            // number of hits to cluster

  // get the lang from request
  String queryLang = request.getParameter("lang");
  if (queryLang == null) { queryLang = ""; }
  Query query = Query.parse(queryString, queryLang, nutchConf);
  bean.LOG.info("query: " + queryString);
  bean.LOG.info("lang: " + queryLang);

  String language =
    ResourceBundle.getBundle("org.nutch.jsp.search", request.getLocale())
    .getLocale().getLanguage();
  String requestURI = HttpUtils.getRequestURL(request).toString();
  String base = requestURI.substring(0, requestURI.lastIndexOf('/'));
  String rss = "../opensearch?query="+htmlQueryString
    +"&hitsPerSite="+hitsPerSite+"&lang="+queryLang+params;
%>
<%
  // To prevent the character encoding declared with 'contentType' page
  // directive from being overriden by JSTL (apache i18n), we freeze it
  // by flushing the output buffer. 
  // see http://java.sun.com/developer/technicalArticles/Intl/MultilingualJSP/
  out.flush();
%>
<%@ taglib uri="http://jakarta.apache.org/taglibs/i18n" prefix="i18n" %>
<i18n:bundle baseName="org.nutch.jsp.search"/>

<%
   // how many hits to retrieve? if clustering is on and available,
   // take "hitsToCluster", otherwise just get hitsPerPage
   int hitsToRetrieve = (clusteringAvailable && clustering.equals("yes") ? hitsToCluster : hitsPerPage);

   if (clusteringAvailable && clustering.equals("yes")) {
     bean.LOG.info("Clustering is on, hits to retrieve: " + hitsToRetrieve);
   }

   // perform query
    // NOTE by Dawid Weiss:
    // The 'clustering' window actually moves with the start
    // position.... this is good, bad?... ugly?....
   Hits hits;
   try{
     hits = bean.search(query, start + hitsToRetrieve, hitsPerSite, "site",
                        sort, reverse);
   } catch (IOException e){
     hits = new Hits(0,new Hit[0]);	
   }
   int end = (int)Math.min(hits.getLength(), start + hitsPerPage);
   int length = end-start;
   int realEnd = (int)Math.min(hits.getLength(), start + hitsToRetrieve);

   Hit[] show = hits.getHits(start, realEnd-start);
   HitDetails[] details = bean.getDetails(show);
   Summary[] summaries = bean.getSummary(details, query);
   bean.LOG.info("total hits: " + hits.getTotal());
%>
<total><%=new Long(hits.getTotal())%></total>
<start><%=new Long((end==0)?0:(start+1))%></start>
<end><%=new Long(end)%></end>
<%
// be responsive
out.flush();
%>
<%
  for (int i = 0; i < length; i++) {      // display the hits
    Hit hit = show[i];
    HitDetails detail = details[i];
    String title = Entities.encode(detail.getValue("title"));
    String url = detail.getValue("url");
    String id = "idx=" + hit.getIndexNo() + "&id=" + hit.getUniqueKey();
    String summary = summaries[i].toHtml(true);
    String caching = detail.getValue("cache");
    
    String bTitle = (new sun.misc.BASE64Encoder()).encode(title.getBytes());
    String bUrl = (new sun.misc.BASE64Encoder()).encode(url.getBytes());
    String bId = (new sun.misc.BASE64Encoder()).encode(id.getBytes());
    String bSummary = (new sun.misc.BASE64Encoder()).encode(summary.getBytes());    
    String more = new String("");
    boolean showSummary = true;
    boolean showCached = true;
    if (caching != null) {
      showSummary = !caching.equals(Nutch.CACHING_FORBIDDEN_ALL);
      showCached = !caching.equals(Nutch.CACHING_FORBIDDEN_NONE);
    }

    if (title == null || title.equals("")) {      // use url for docs w/o title
      title = url;
    }
    %>
    
    <% if (hit.moreFromDupExcluded()) {
    more =
    "query="+URLEncoder.encode("site:"+hit.getDedupValue()+" "+queryString, "UTF8")
    +params+"&hitsPerSite="+0
    +"&lang="+queryLang
    +"&clustering="+clustering;
    %> 
    <% } %>
    <%
    String pageCache = "../cached.jsp?" + id;
    String explain = "../explain.jsp?" + id +"&query=" + URLEncoder.encode(queryString, "UTF-8") + "&lang=" + queryLang;
    String moreResult = "../query.jsp?" + more;
    String bMore = (new sun.misc.BASE64Encoder()).encode(moreResult.getBytes());
    String bCache = (new sun.misc.BASE64Encoder()).encode(pageCache.getBytes());
    String bExplain = (new sun.misc.BASE64Encoder()).encode(explain.getBytes());
    %>
    
    <Record>
    <t><%=Entities.encode(bTitle)%></t>
    <u><%=Entities.encode(bUrl)%></u>
    <summary><%=Entities.encode(bSummary)%></summary>
    <cache><%=Entities.encode(bCache)%></cache>
    <explain><%=Entities.encode(bExplain)%></explain>
    <more><%=Entities.encode(bMore)%></more>   
  </Record>
<% } %>
</Root>
