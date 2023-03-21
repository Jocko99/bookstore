export const logoutMixin = {
    methods:{
        logout(){
          if(localStorage.getItem('actor'))
          {
            localStorage.removeItem('actor');
            localStorage.removeItem('accessToken');
            this.$router.go();
          }
        }  
    }
}