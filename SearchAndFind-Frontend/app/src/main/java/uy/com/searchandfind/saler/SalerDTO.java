package uy.com.searchandfind.saler;
import java.util.Collection;

import uy.com.searchandfind.user.UserDTO;

public class SalerDTO extends UserDTO {

    private double Latitude ;
    private double Length ;
    private String ShopName ;
    private String ShopAddress ;
    private String ShopPhone ;
    private int NumberOfReview;
    private double AverageReview ;
    private int ShopHourOpen ;
    private int ShopHourClose;
    private String ShopDaysOpen;


    public int getShopHourOpen() {
        return ShopHourOpen;
    }

    public void setShopHourOpen(int shopHourOpen) {
        ShopHourOpen = shopHourOpen;
    }

    public int getShopHourClose() {
        return ShopHourClose;
    }

    public void setShopHourClose(int shopHourClose) {
        ShopHourClose = shopHourClose;
    }

    public String getShopDaysOpen() {
        return ShopDaysOpen;
    }

    public void setShopDaysOpen(String shopDaysOpen) {
        ShopDaysOpen = shopDaysOpen;
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

    public String getShopName() {
        return ShopName;
    }

    public void setShopName(String shopName) {
        ShopName = shopName;
    }

    public String getShopAddress() {
        return ShopAddress;
    }

    public void setShopAddress(String shopAddress) {
        ShopAddress = shopAddress;
    }

    public String getShopPhone() {
        return ShopPhone;
    }

    public void setShopPhone(String shopPhone) {
        ShopPhone = shopPhone;
    }

    public int getNumberOfReview() {
        return NumberOfReview;
    }

    public void setNumberOfReview(int numberOfReview) {
        NumberOfReview = numberOfReview;
    }

    public double getAverageReview() {
        return AverageReview;
    }

    public void setAverageReview(double averageReview) {
        AverageReview = averageReview;
    }
}
