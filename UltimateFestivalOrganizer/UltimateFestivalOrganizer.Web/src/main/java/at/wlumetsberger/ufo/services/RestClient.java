package at.wlumetsberger.ufo.services;



import java.io.BufferedOutputStream;
import java.io.BufferedWriter;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.Reader;
import java.io.Serializable;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;
import java.util.Map;
import java.util.stream.Stream;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.sun.jndi.toolkit.url.Uri;

import at.wlumetsberger.ufo.models.dataTransfer.IBaseDataTransferObject;
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
		Gson g = new GsonBuilder().setDateFormat("yyyy-MM-dd'T'HH:mm:ss").create();
		try {
			URL url = new URL(this.baseServiceUrl+relativUrl);
			conn = (HttpURLConnection) url.openConnection();
			conn.setRequestMethod("GET");
			conn.setRequestProperty("Accept", "application/json");
			System.out.println("url: "+ url.toString());
			System.out.println("response: "+ conn.getResponseCode() + " / "+ conn.getResponseMessage());
			if (conn.getResponseCode() != 200) {
				throw new ServiceConnectionException(conn.getResponseCode(),conn.getResponseMessage());
			}
			Reader r = new InputStreamReader(conn.getInputStream());
			return g.fromJson(r, clazz);
		} catch (Exception e) {
			if(e instanceof ServiceConnectionException){
				throw (ServiceConnectionException)e;
			}
			e.printStackTrace();
			throw new ServiceConnectionException(0, e.getMessage());
		}finally {
			if(conn != null){
				conn.disconnect();
			}
			
		}
	}
	
	public <T>T sendPostRequest(String relativUrl, String userId, IBaseDataTransferObject obj, Class clazz) throws ServiceConnectionException{
		
		HttpURLConnection connection = null;
		Gson g = new GsonBuilder().setDateFormat("yyyy-MM-dd'T'HH:mm:ss").create();
		try{
			URL url = new URL(this.baseServiceUrl+relativUrl);
			connection = (HttpURLConnection) url.openConnection();
			connection.setRequestMethod("POST");
			connection.setRequestProperty("Content-Type","application/json");
            connection.setRequestProperty("Accept","application/json");
			connection.setRequestProperty("Authorization", "Basic " + userId);
			connection.setDoInput(true);
			connection.setDoOutput(true);
			String data = g.toJson(obj);
			
			OutputStream outStream = new BufferedOutputStream(connection.getOutputStream());
            outStream.write(data.getBytes());
            outStream.flush();
            outStream.close();
            System.out.println("url: "+ url.toString());
			System.out.println("response: "+ connection.getResponseCode() + " / "+ connection.getResponseMessage());
			if (connection.getResponseCode() != 200) {
				throw new ServiceConnectionException(connection.getResponseCode(),connection.getResponseMessage());
			}
			Reader r = new InputStreamReader(connection.getInputStream());
			return g.fromJson(r, clazz);
		}catch(Exception e){
			if(e instanceof ServiceConnectionException){
				throw (ServiceConnectionException)e;
			}
			throw new ServiceConnectionException(0,e.getMessage());
		}finally{
			if(connection != null){
				connection.disconnect();
			}
		}
	}
    
	
	public <T>T fetchJsonData(String relativUrl,Map<String,String>params,String userId, Class clazz)throws ServiceConnectionException{
		HttpURLConnection conn = null;
		Gson g = new GsonBuilder().setDateFormat("yyyy-MM-dd'T'HH:mm:ss").create();
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
			conn.setRequestProperty("Accept", "application/json");
			if(userId != null){
				conn.setRequestProperty("Authorization", "Basic " + userId);
			}
							conn.setRequestMethod("GET");
						
			System.out.println("url: "+ url.toString());
			System.out.println("response: "+ conn.getResponseCode() + " / "+ conn.getResponseMessage());
			int  statusCode= conn.getResponseCode();
			
			if (statusCode != 200) {
				throw new ServiceConnectionException(statusCode,conn.getResponseMessage());
			}
			Reader r = new InputStreamReader(conn.getInputStream());
			return g.fromJson(r, clazz);
		} catch (Exception e) {
			if(e instanceof ServiceConnectionException){
				throw (ServiceConnectionException)e;				
			}		
			throw new ServiceConnectionException(0, e.getMessage());
		}finally {
			if(conn != null){
				conn.disconnect();
			}
			
		}
		
	}
	
}
