package com.studentmajorleague;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.ListView;

public class UsersActivity extends AppCompatActivity {

    String[] users = { "Denis Pasyuga", "Test Test" };

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_users);

        ListView view = (ListView) findViewById(R.id.view_users);

        ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,
                android.R.layout.simple_list_item_1, users);

        view.setAdapter(adapter);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu){
        getMenuInflater().inflate(R.menu.main, menu);

        menu.findItem(R.id.nav_leagues).setOnMenuItemClickListener(new MenuItem.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                Intent intent = new Intent(UsersActivity.this, LeaguesActivity.class);
                startActivity(intent);

                return true;
            }
        });

        menu.findItem(R.id.nav_competitions).setOnMenuItemClickListener(new MenuItem.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                Intent intent = new Intent(UsersActivity.this, CompetitionsActivity.class);
                startActivity(intent);

                return true;
            }
        });

        menu.findItem(R.id.nav_exit).setOnMenuItemClickListener(new MenuItem.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                Intent intent = new Intent(UsersActivity.this, MainActivity.class);
                startActivity(intent);

                return true;
            }
        });

        return true;
    }
}
