<template>
  <v-app>
    <Header/>
    <Navigation/>
    <v-main>
      <v-container fluid>
        <router-view></router-view>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import Header from './components/layout/Header.vue';
import Navigation from './components/layout/Navigation.vue'
import {logoutMixin} from './mixins/logoutMixins';
export default {
  name: 'App',

  components:{
        Header,
        Navigation,
  },
  data: () => ({
    //
  }),
    mixins:[logoutMixin],
    beforeMount:function(){
        setInterval(()=>{
          this.autoLogout()
        },9000);
    },
    methods:{
        autoLogout(){
          if(localStorage.getItem('accessToken')){
            let time = this.currentTime();
            let expTime = this.tokenExp();
            if(expTime<time){
                this.logout();
            }
          }
        },
        currentTime(){
            return Math.floor((new Date()).getTime() / 1000);
        },
        tokenExp(){
         let token = localStorage.getItem("accessToken").split('.')[1];
         let claim = JSON.parse(atob(token));
         let time = claim.exp;
         return parseInt(time);
        },
    },
    computed:{
    },
};
</script>
