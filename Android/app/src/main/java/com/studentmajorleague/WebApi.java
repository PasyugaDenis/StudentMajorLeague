package com.studentmajorleague;

import com.studentmajorleague.models.UserModel;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface WebApi {
    @POST("/api/User/Authorization")
    Call<Object> authorization(@Body UserModel user);
}
