package com.studentmajorleague;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.ArrayAdapter;
import android.widget.ListView;

public class LeaguesActivity extends AppCompatActivity {
    String[] leagues = { "NCAA", "NCAA Division 1", "NCAA Division 2", "NCAA Division 3",
            "NAIA", "NAIA Division 1", "NAIA Division 2",
            "NJCAA", "NJCAA Division 1", "NJCAA Division 2", "NJCAA Division 3"};

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_leagues);

        ListView view = (ListView) findViewById(R.id.view_leagues);

        ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,
                android.R.layout.simple_list_item_1, leagues);

        view.setAdapter(adapter);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu){
        getMenuInflater().inflate(R.menu.main, menu);

        menu.findItem(R.id.nav_users).setOnMenuItemClickListener(new MenuItem.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                Intent intent = new Intent(LeaguesActivity.this, UsersActivity.class);
                startActivity(intent);

                return true;
            }
        });

        menu.findItem(R.id.nav_competitions).setOnMenuItemClickListener(new MenuItem.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                Intent intent = new Intent(LeaguesActivity.this, CompetitionsActivity.class);
                startActivity(intent);

                return true;
            }
        });

        menu.findItem(R.id.nav_exit).setOnMenuItemClickListener(new MenuItem.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                Intent intent = new Intent(LeaguesActivity.this, MainActivity.class);
                startActivity(intent);

                return true;
            }
        });

        return true;
    }
}
