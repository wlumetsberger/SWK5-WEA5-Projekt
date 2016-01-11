package at.wlumetsberger.ufo.services;

import java.util.Arrays;
import java.util.List;

import javax.faces.bean.ManagedProperty;
import javax.inject.Inject;

import com.google.gson.Gson;

import at.wlumetsberger.ufo.beans.UserBean;
import at.wlumetsberger.ufo.models.Artist;
import at.wlumetsberger.ufo.services.exceptions.ServiceConnectionException;
import at.wlumetsberger.ufo.services.interfaces.IQueryService;
import lombok.Getter;
import lombok.Setter;

public class QueryService implements IQueryService {

	private RestClient restClient;
	
	
	public QueryService(RestClient client) {
		this.restClient = client;
	}

	public List<Artist> queryArtists() throws ServiceConnectionException {
		Artist[] result = restClient.<Artist[]>fetchJsonData("artist",Artist[].class);
		return Arrays.asList(result);
	}

}
