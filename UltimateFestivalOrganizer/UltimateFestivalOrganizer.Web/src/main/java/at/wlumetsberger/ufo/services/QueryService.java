package at.wlumetsberger.ufo.services;

import java.text.SimpleDateFormat;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.time.format.DateTimeFormatterBuilder;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import at.wlumetsberger.ufo.models.Artist;
import at.wlumetsberger.ufo.models.Catagory;
import at.wlumetsberger.ufo.models.Performance;
import at.wlumetsberger.ufo.models.Venue;
import at.wlumetsberger.ufo.services.exceptions.ServiceConnectionException;
import at.wlumetsberger.ufo.services.interfaces.IQueryService;

public class QueryService implements IQueryService {

	private static final long serialVersionUID = -4505103870287898720L;
	private RestClient restClient;
	
	
	public QueryService(RestClient client) {
		this.restClient = client;
	}

	public List<Artist> queryArtists() throws ServiceConnectionException {
		Artist[] result = restClient.<Artist[]>fetchJsonData("artist",Artist[].class);
		return Arrays.asList(result);
	}

	
	@Override
	public List<Catagory> queryCatagories() throws ServiceConnectionException {
		Catagory[] catagory = restClient.<Catagory[]>fetchJsonData("catagory",Catagory[].class);
		return Arrays.asList(catagory);
	}

	@Override
	public List<Venue> queryVenues() throws ServiceConnectionException {
		Venue[] venue = restClient.<Venue[]>fetchJsonData("venue",Venue[].class);
		return Arrays.asList(venue);
	}

	@Override
	public List<Performance> queryPerformancesByDay(LocalDate day) throws ServiceConnectionException {
		Map<String,String> params = new HashMap<>();
		DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd");
		params.put("day", formatter.format(day) );
		Performance[] performances = restClient.<Performance[]>fetchJsonData("performance", params,null,Performance[].class);
		return Arrays.asList(performances);
		
	}

	@Override
	public List<Performance> queryPerformancesByArtist(int artistId) throws ServiceConnectionException {
		Map<String,String> params = new HashMap<>();
		params.put("artistId", String.valueOf(artistId));
		Performance[] performances = restClient.<Performance[]>fetchJsonData("performance", params,null,Performance[].class);
		return Arrays.asList(performances);
	}

	@Override
	public List<Performance> queryPerformancesByVenue(int venueId) throws ServiceConnectionException {
		Map<String,String> params = new HashMap<>();
		params.put("venueId", String.valueOf(venueId));
		Performance[] performances = restClient.<Performance[]>fetchJsonData("performance", params,null,Performance[].class);
		return Arrays.asList(performances);
	}

	@Override
	public List<Performance> queryPerformancesByCatagory(int catagoryId) throws ServiceConnectionException {
		Map<String,String> params = new HashMap<>();
		params.put("catagoryId", String.valueOf(catagoryId));
		Performance[] performances = restClient.<Performance[]>fetchJsonData("performance", params,null,Performance[].class);
		return Arrays.asList(performances);
	}

}
