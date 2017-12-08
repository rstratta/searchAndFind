package uy.com.searchandfind.query;

import java.security.Timestamp;
import java.util.List;

/**
 * Created by strat on 24/10/2017.
 */

public class QueryDTO {
    private String Id ;
    private String CategoryName ;
    private String QueryDate ;
    private String Description ;
    private String State ;
    private double Latitude ;
    private double Length ;
    private String CategoryId ;
    private SalerAvailableForTenderDTO[] Salers ;

    public String getId() {
        return Id;
    }

    public void setId(String id) {
        Id = id;
    }

    public String getCategoryName() {
        return CategoryName;
    }

    public void setCategoryName(String categoryName) {
        CategoryName = categoryName;
    }

    public String getQueryDate() {
        return QueryDate;
    }

    public void setQueryDate(String queryDate) {
        QueryDate = queryDate;
    }

    public String getDescription() {
        return Description;
    }

    public void setDescription(String description) {
        Description = description;
    }

    public String getState() {
        return State;
    }

    public void setState(String state) {
        State = state;
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

    public String getCategoryId() {
        return CategoryId;
    }

    public void setCategoryId(String categoryId) {
        CategoryId = categoryId;
    }

    public SalerAvailableForTenderDTO[] getSalers() {
        return Salers;
    }

    public void setSalers(SalerAvailableForTenderDTO[] salers) {
        Salers = salers;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        QueryDTO queryDTO = (QueryDTO) o;

        return Id != null ? Id.equals(queryDTO.Id) : queryDTO.Id == null;

    }

    @Override
    public int hashCode() {
        return Id != null ? Id.hashCode() : 0;
    }
}
