package at.wlumetsberger.ufo.services;



import java.io.InputStreamReader;
import java.io.Reader;
import java.io.Serializable;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;
import java.util.Map;

import com.google.gson.Gson;

import at.wlumetsberger.ufo.services.exceptions.ServiceConnectionException;
import at.wlumetsberger.ufo.util.ApplicationConfiguration;
import lombok.Getter;
import lombok.Setter;


public class RestClient implements Serializable {
	
	/**
	 * 
	 */
	private static final long serialVersionUID = 5251008977944854832L;
	@Getter @Setter
	private String baseServiceUrl;
	
	public RestClient(){
		this.initialize();
	}
	
	private void initialize(){
		baseServiceUrl = ApplicationConfiguration.getInstance().getRestServiceUrl();
	}
	
	public <T>T fetchJsonData(String relativUrl, Class clazz)throws ServiceConnectionException{
		HttpURLConnection conn = null;
		Gson g = new Gson();
		try {
			URL url = new URL(this.baseServiceUrl+relativUrl);
			conn = (HttpURLConnection) url.openConnection();
			conn.setRequestMethod("GET");
			conn.setRequestProperty("Accept", "application/json");
			if (conn.getResponseCode() != 200) {
				throw new ServiceConnectionException(conn.getResponseCode(),conn.getResponseMessage());
			}
			Reader r = new InputStreamReader(conn.getInputStream());
			return g.fromJson(r, clazz);
		} catch (Exception e) {
			e.printStackTrace();
			throw new ServiceConnectionException(0, e.getMessage());
		}finally {
			if(conn != null){
				conn.disconnect();
			}
			
		}
	}
	public <T>T fetchJsonData(String relativUrl,Map<String,String>params,String userId, Class clazz)throws ServiceConnectionException{
		HttpURLConnection conn = null;
		Gson g = new Gson();
		boolean first = true;
		try {
			StringBuilder queryString = new StringBuilder(this.baseServiceUrl);
			queryString.append(relativUrl);
			
			if(params != null){
				for(Map.Entry<String, String> entry : params.entrySet()){
					if(first){
						queryString.append("?");
						first = false;
					}else{
						queryString.append("&");
					}
					queryString.append(URLEncoder.encode(entry.getKey(),"UTF-8"));
					queryString.append("=");
					queryString.append(URLEncoder.encode(entry.getValue(),"UTF-8"));
					
				}
			}
			URL url = new URL(queryString.toString());
			conn = (HttpURLConnection) url.openConnection();
			conn.setRequestMethod("GET");
			conn.setRequestProperty("Accept", "application/json");
			if(userId != null){
				conn.setRequestProperty("Authorization", "Basic " + userId);
			}
			if (conn.getResponseCode() != 200) {
				throw new ServiceConnectionException(conn.getResponseCode(),conn.getResponseMessage());
			}
			Reader r = new InputStreamReader(conn.getInputStream());
			return g.fromJson(r, clazz);
		} catch (Exception e) {
			//e.printStackTrace();
			throw new ServiceConnectionException(0, e.getMessage());
		}finally {
			if(conn != null){
				conn.disconnect();
			}
			
		}
		
	}
	
}
