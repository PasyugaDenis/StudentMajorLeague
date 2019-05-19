package com.studentmajorleague;

import retrofit2.Retrofit;

public class NetworkService {
    private static NetworkService mInstance;
    private static final String BASE_URL = "http://studentmajorleaguebeta";
    private Retrofit mRetrofit;

    private NetworkService() {
        mRetrofit = new Retrofit.Builder()
            .baseUrl(BASE_URL)
            .build();
    }

    public static NetworkService getInstance() {
        if (mInstance == null) {
            mInstance = new NetworkService();
        }
        return mInstance;
    }

    public WebApi getApi() {
        return mRetrofit.create(WebApi.class);
    }
}
