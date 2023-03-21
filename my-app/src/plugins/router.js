import Vue from 'vue';
import VueRouter from 'vue-router';
import Books from '../views/Books.vue';
import Authors from '../views/Authors.vue';
import Login from '../views/Login.vue';
import Genres from '../views/Genres.vue';
import Users from '../views/Users.vue';

Vue.use(VueRouter);

const routes = [
    { path: "/", name:'login', component: Login},
    { path: "/books", name:'books', component: Books , meta:{ requiresAuth:true }},
    { path: "/authors", name:'authors', component: Authors, meta:{ requiresAuth:true }},
    { path: "/genres", name:'genres', component: Genres, meta:{ requiresAuth:true }},
    { path: "/users", name:'users', component: Users, meta:{ requiresAuth:true }},
];

const router = new VueRouter({
    routes
  })

router.beforeEach((to,from,next)=>{
    if(to.matched.some(record => record.meta.requiresAuth) && !localStorage.getItem('accessToken')){
        // if(){
            next({
                name:'login'
            }); 
        // }
    }else{
        next();
    }
});
  
  export default router;
