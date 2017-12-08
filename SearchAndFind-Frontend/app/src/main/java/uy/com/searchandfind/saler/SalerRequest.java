package uy.com.searchandfind.saler;

import java.util.Collection;

import uy.com.searchandfind.category.CategoryDTO;
import uy.com.searchandfind.user.UserDTO;
import uy.com.searchandfind.user.UserRequest;

/**
 * Created by strat on 30/09/2017.
 */

public class SalerRequest extends UserRequest{

    private double Latitude;
    private double Length;
    private String ShopName;
    private String ShopAddress;
    private String ShopPhone;
    private int ShopHourOpen;
    private int ShopHourClose;
    private String ShopDaysOpen;
    private Collection<CategoryDTO> Categories;
    private Collection<SalerCategoryDTO> SalerCategoryDTO;

    public SalerRequest(){}
    public SalerRequest(UserDTO user){
        if(user!=null) {
            setName(user.getName());
            setLastName(user.getLastName());
            setDeviceId(user.getDeviceId());
            setMail(user.getMailAddress());
            setUserId(user.getId());
        }
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
        this.ShopName = shopName;
    }

    public String getShopAddress() {
        return ShopAddress;
    }

    public void setShopAddress(String shopAddress) {
        this.ShopAddress = shopAddress;
    }

    public String getShopPhone() {
        return ShopPhone;
    }

    public void setShopPhone(String shopPhone) {
        this.ShopPhone = shopPhone;
    }

    public double getShopHourOpen() {
        return ShopHourOpen;
    }

    public void setShopHourOpen(int shopHourOpen) {
        this.ShopHourOpen = shopHourOpen;
    }

    public double getShopHourClose() {
        return ShopHourClose;
    }

    public void setShopHourClose(int shopHourClose) {
        this.ShopHourClose = shopHourClose;
    }

    public String getShopDaysOpen() {
        return ShopDaysOpen;
    }

    public void setShopDaysOpen(String shopDaysOpen) {
        this.ShopDaysOpen = shopDaysOpen;
    }
    public Collection<CategoryDTO> getCategories() {
        return Categories;
    }

    public void setCategories(Collection<CategoryDTO> categories) {
        this.Categories = categories;
    }

   public Collection<SalerCategoryDTO>  getSalerCategoryDTO() {
        return SalerCategoryDTO;
    }

    public void setSalerCategoryDTO(Collection<SalerCategoryDTO>  salerCategoryDTO) {
        this.SalerCategoryDTO = salerCategoryDTO;
    }
}