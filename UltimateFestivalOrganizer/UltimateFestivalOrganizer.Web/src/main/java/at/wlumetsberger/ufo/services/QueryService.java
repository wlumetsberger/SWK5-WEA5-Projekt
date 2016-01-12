package at.wlumetsberger.ufo.services;

import java.util.Arrays;
import java.util.List;

import at.wlumetsberger.ufo.models.Artist;
import at.wlumetsberger.ufo.services.exceptions.ServiceConnectionException;
import at.wlumetsberger.ufo.services.interfaces.IQueryService;

public class QueryService implements IQueryService {

	/**
	 * 
	 */
	private static final long serialVersionUID = -4505103870287898720L;
	private RestClient restClient;
	
	
	public QueryService(RestClient client) {
		this.restClient = client;
	}

	public List<Artist> queryArtists() throws ServiceConnectionException {
		Artist[] result = restClient.<Artist[]>fetchJsonData("artist",Artist[].class);
		return Arrays.asList(result);
	}

}
