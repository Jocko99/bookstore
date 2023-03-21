export default {
    computed:{
        $loggedUser() {
            var localStorageUser = localStorage.getItem("actor");
            if(localStorage){
                return JSON.parse(localStorageUser);
            }
              return null;
         },
         $jwt(){
            return localStorage.getItem("accessToken");
        }
    }
}

