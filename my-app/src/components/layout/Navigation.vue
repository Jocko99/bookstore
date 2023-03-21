<template>
    <div v-if="this.$loggedUser">
        <v-navigation-drawer
      v-model="drawer"  
      app
              color="blue-grey lighten-5"
    >
        <v-list
        nav
        dense
      >
        <v-list-item-group
          v-for="(nav,i) in navigationItems"
          :key="i"
        >
          <v-list-item :to="nav.href">
            <v-list-item-title>{{nav.name}}</v-list-item-title>
          </v-list-item>
        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>

    <v-app-bar app color="blue-grey lighten-5">
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title>Book store</v-toolbar-title>
      
    </v-app-bar>
  </div>
</template>
<script>
  export default {
    data: () => ({ 
        drawer: null,
        navigationItems:[
            {
                'name':'Books',
                'href':'/books'
            },
            {
                'name':'Authors',
                'href':'/authors'
            },
            {
                'name':'Genres',
                'href':'/genres'
            },
            {
                'name':'Users',
                'href':'/users'
            }
        ]
        }),
  }
</script>