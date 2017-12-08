package uy.com.searchandfind.category;

/**
 * Created by Fede2 on 14/10/2017.
 */

public class CategoryDTO {
    private String Id;
    private String CategoryName;
    private String CategoryDescription;


    public void setId(String id) {
        this.Id = id;
    }

    public String getId() {
        return Id;
    }

    public void setCategoryName(String categoryName) {
        this.CategoryName = categoryName;
    }

    public String getCategoryName() {
        return CategoryName;
    }

    public void setCategoryDescription(String categoryDescription) {
        this.CategoryDescription = categoryDescription;
    }

    public String getCategoryDescription() {
        return CategoryDescription;
    }


}
