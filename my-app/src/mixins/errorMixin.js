export default {
    methods:{
        $errorWrite(error){
            let errors = error.response.data.errors;
            let message = "";
                for(let i = 0; i < errors.length; i++){
                  message +=errors[i].ErrorMessage;
                }
            this.warningError = message;
          }
    }
}