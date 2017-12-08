package uy.com.searchandfind.category;

import java.util.List;

import uy.com.searchandfind.Response;

/**
 * Created by strat on 23/10/2017.
 */

public class CategoryResponse extends Response {
    private CategoryDTO[] Categories;

    public CategoryResponse(String message){
        super(message);
    }

    public CategoryDTO[] getCategories() {
        return Categories;
    }

    public void setCategories(CategoryDTO[] categories) {
        Categories = categories;
    }
}
