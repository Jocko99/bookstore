<template>
    <v-container v-if='!this.$loggedUser'>
        <v-row class="d-flex justify-center mt-5">
        <v-col xl="6" lg="6" md="6" sm="6" xs="12" class="text-center mt-5">
            <v-card elevation="6" class="mt-5">
                    <v-card-title class="mb-3 text-h5 font-weight-medium blue-grey white--text">Login</v-card-title>
                    <div class="pa-4">
                        <v-form ref="form" v-model="valid" lazy-validation> 
                            <v-text-field
                            color="#264D61"
                            v-model="userData.username"
                            :rules="[rules.required,rules.usernameMinLength]"
                            counter="50"
                            label="Username"
                            maxlength="50"
                            required  
                            ></v-text-field>
                            <v-text-field
                            color="#264D61"
                            v-model="userData.password"
                            :rules="[rules.required,rules.passwordLength]"
                            :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
                            label="Password"
                            required
                            counter
                            :type="show1 ? 'text' : 'password'"
                            @click:append="show1 = !show1"
                            ></v-text-field>
                    <v-btn @click="clear" class="mr-4" :disabled="valid">
                        Cancel
                    </v-btn>
                    <v-btn
                        :disabled="!valid"
                        type="submit"
                        @click="submit"
                    >
                        Confirm
                    </v-btn>
                </v-form>
                    </div>
            </v-card>            
        </v-col>
        </v-row>
        <v-row>
            <v-col cols="12" class="d-flex justify-center">
                <v-alert
                v-bind="warningError"
                v-if="warningError.length"
                color="orange"
                dismissible
                elevation="4"
                type="warning"
                >
                {{warningError}}
                </v-alert>
            </v-col>
        </v-row>
    </v-container>
</template>
 
<script>
    import axios from 'axios';
    export default{
        data:() => ({
            valid:true,
            show1:false, 
            userData:{
                username:'',
                password:'',
            },
            warningError:'',
            formHasErrors: false,
            rules:{
                required: value => !!value || 'This field is required.',
                usernameMinLength: value => value.length >= 3 || 'Minimum lentgh for username is 3.',
                passwordLength: value => value.length >= 6 || 'Minimum password length is 6.',
            },
        }),
        methods:{
            submit () {  
               this.$refs.form.validate();
               this.login();
            },
            clear () {
                this.$refs.form.reset()
            },
            login(){
                axios
                    .post(process.env.VUE_APP_ROOT_API+"login",this.userData)
                    .then((response) => {
                        var token = response.data.token;
                        var userClaimData = token.split('.')[1];
                        var claimObject = JSON.parse(atob(userClaimData));
                        localStorage.setItem("accessToken",token);
                        localStorage.setItem("actor",claimObject.ActorData);   
                        this.$router.go();
                    })
                    .catch(()=>{
                        this.warningError = "Username or password is incorrect.";
                    })
            }
        },
        
    }
</script>