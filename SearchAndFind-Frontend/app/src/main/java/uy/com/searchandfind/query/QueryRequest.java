package uy.com.searchandfind.query;

import uy.com.searchandfind.Request;

/**
 * Created by strat on 24/10/2017.
 */

public class QueryRequest extends Request {
    private String QueryId;
    private String Category ;
    private String Descritpion;
    private double Latitude ;
    private double Length;

    public String getCategory() {
        return Category;
    }

    public void setCategory(String category) {
        Category = category;
    }

    public String getDescritpion() {
        return Descritpion;
    }

    public void setDescritpion(String descritpion) {
        Descritpion = descritpion;
    }

    public double getLatitude() {
        return Latitude;
    }

    public void setLatitude(double latitude) {
        Latitude = latitude;
    }

    public double getLength() {
        return Length;
    }

    public void setLength(double length) {
        Length = length;
    }

    public String getQueryId() {
        return QueryId;
    }

    public void setQueryId(String queryId) {
        QueryId = queryId;
    }
}
