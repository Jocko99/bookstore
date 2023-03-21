<template>
  <div>
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
    <v-data-table
    :headers="headers"
    :items="authors"
    :page.sync="searchObj.page"
    :items-per-page="searchObj.perPage"
    hide-default-footer
    @page-count="pageCount = $event"
    sort-by="name"
    class="elevation-1"
  >
    <template v-slot:top>
      <v-toolbar
        flat
      >
        <v-toolbar-title>Authors</v-toolbar-title>
        <v-divider
          class="mx-4"
          inset
          vertical
        ></v-divider>
        <v-spacer></v-spacer>
        <v-text-field
        color="#00919e"
        v-model="searchObj.keyword"
        append-icon="mdi-magnify"
        label="Pretraga"
        single-line
        hide-details
        @keyup="initialize"
      ></v-text-field>
        <v-dialog
          v-model="dialog"
          max-width="600px"
        >
          <template v-slot:activator="{ on, attrs }">
            <v-btn
              color="primary"
              dark
              class="mb-2 ml-5"
              v-bind="attrs"
              v-on="on"
            >
              <v-icon>
                mdi-plus
              </v-icon>
              Add
            </v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="text-h5">{{ formTitle }}</span>
            </v-card-title>
            <v-card-text>
              <v-form ref="form" v-model="valid">
                <v-container>
                <v-row>
                  <v-col
                    cols="12"
                    sm="6"
                    md="4"
                  >
                    <v-text-field
                      v-model="editedItem.firstName"
                      :rules="[rules.required,rules.min]"
                      counter
                      maxlength="50"
                      label="First name"
                    ></v-text-field>
                  </v-col>
                  <v-col
                    cols="12"
                    sm="6"
                    md="4"
                  >
                    <v-text-field
                      v-model="editedItem.lastName"
                      :rules="[rules.required,rules.min]"
                      counter
                      maxlength="50"
                      label="Last name"
                    ></v-text-field>
                  </v-col>
                  <v-col
                    cols="12"
                    sm="6"
                    md="4"
                    v-if="!hideDiv"
                  >
                    <v-file-input
                     :rules="[rules.required,rules.photo]"
                     accept="image/png, image/jpeg, image/bmp, image/jpg, image/svg"
                     @change="handleFile"
                     
                     >
                    </v-file-input>
                    
                  </v-col>
                  <v-col
                    cols="12"
                    sm="6"
                    md="4"
                    v-if="hideDiv"
                  >
                    <v-img v-if="photoForRepreset" :src="photoForRepreset" width="150"></v-img>
                    <v-img v-else :src="previewImage" width="150"></v-img>
                    <v-icon small color="red" @click="removeAddedPhoto">
                      mdi-delete
                    </v-icon>
                  </v-col>
                  <v-col
                    cols="12"
                    sm="6"
                    md="4"
                  >
                    <v-textarea
                      v-model="editedItem.biography"
                      :rules="[rules.required,rules.min]"
                      counter
                      maxlength="3000"
                      label="Biography"
                    ></v-textarea>
                  </v-col>
                </v-row>
              </v-container>
              </v-form>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn
                color="blue darken-1"
                text
                @click="close"
              >
                Zatvori
              </v-btn>
              <v-btn
                :disabled="!valid"
                color="blue darken-1"
                text
                @click="save(editedItem.id)"
              >
                Sačuvaj
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>      
        <v-dialog v-model="dialogDelete" max-width="600px">
          <v-card>
            <v-card-title class="text-h5">Are you sure you want to delete this item?</v-card-title>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="closeDelete">Cancel</v-btn>
              <v-btn color="blue darken-1" text @click="deleteItemConfirm">Delete</v-btn>
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>
    <template v-slot:item.coverPhoto="{ item }">
        <v-img :src="'../assets/images/' + item.coverPhoto" style="width: 50px; height: 50px" :alt="`${item.firstName} ${item.lastName}`"></v-img>
    </template>
    <template v-slot:item.actions="{ item }">
      <v-icon
        small
        class="mr-2 theme--light"
        @click="editItem(item)"
      >
        mdi-pencil
      </v-icon>
      <v-icon
        small
        @click="deleteItem(item)"
        color="red"
      >
        mdi-delete
      </v-icon>
    </template>
  </v-data-table>
  <div class="text-center pt-2" v-if="authors.length">
      <v-pagination
        v-model="searchObj.page"
        :length="pageCount"
      ></v-pagination>
      <v-row
          class="mt-2"
          align="center"
          justify="end"
        >
          <span class="grey--text">Broj stavki po strani:</span>
          <v-menu offset-y>
            <template v-slot:activator="{ on, attrs }">
              <v-btn
                dark
                text
                color="primary"
                class="ml-2"
                v-bind="attrs"
                v-on="on"
              >
                {{ searchObj.perPage }}
                <v-icon>mdi-chevron-down</v-icon>
              </v-btn>
            </template>
            <v-list>
              <v-list-item
                v-for="(number, index) in itemsPerPageArray"
                :key="index"
                @click="updateItemsPerPage(number)"
              >
                <v-list-item-title>{{ number }}</v-list-item-title>
              </v-list-item>
            </v-list>
          </v-menu>
        </v-row>
    </div>  
  </div>
</template>

<script>
  import axios from 'axios';
  import { photoMixin } from '../mixins/componentMixins';

  export default {
    data: () => ({
      searchObj: {
        keyword: null,
        page: 1,
        perPage: 10
      },
      valid:true,
      hideDiv:false,
      // previewImage:null,
      pageCount: 1,
      dialog: false,
      dialogDelete: false,
      itemsPerPageArray: [5, 10, 15],
      warningError:'',
      lastId:0,
      headers: [
        {
          text: 'First name',
          align: 'start',
          value: 'firstName',
        },
        { text: 'Last name', value: 'lastName' },
        { text: 'Cover Photo', value: 'coverPhoto' },
        { text: 'Biography', value: 'biography' },
        { text: 'Actions', value: 'actions', sortable: false },
      ],
      authors: [],
      editedIndex: -1,
      defaultItem: {
        id:0,
        firstName:"",
        lastName:"",
        coverPhoto:"",
        biography:"",
      },
      rules: {
          required: value => !!value || 'This field is required.',
          min: value => value.length >= 3 || 'Minimum length is 3.',
          photo: value => !value || value.size < 2000000 || 'Image size should be less than 2 MB!',
      },
      editedItem: {
        id:0,
        firstName:"",
        lastName:"",
        coverPhoto:"",
        biography:"",
      },
    }),
    mixins:[photoMixin],
    computed: {
      formTitle () {
        return this.editedIndex === -1 ? 'New item' : 'Edit item'
      },
      // photoForRepreset(){
      //     return this.editedIndex === -1 ?  "" : "../assets/images/" + this.editedItem.coverPhoto
      // }
    },
    watch: {
      dialog (val) {
        val || this.close()
      },
      dialogDelete (val) {
        val || this.closeDelete()
      },
    },

    created () {
      this.initialize()
    },

    methods: {
      initialize () {
         axios
          .get(process.env.VUE_APP_ROOT_API+"author", {
               headers: { Authorization: "Bearer " + this.$jwt},
               params: this.searchObj 
               }).then((response) => {
            this.authors = response.data.items;
            this.page = response.data.page;
            this.pageCount = response.data.pagesCount;
          }).catch((error) =>{
            console.log(error);
          });
      },
      editItem(item){  
        this.editedIndex = this.authors.indexOf(item)
        this.editedItem = Object.assign({}, item)
        console.log(this.editedItem)
        this.hideDiv = true;
        // this.navigationId = item.id;
        this.dialog = true
      },
      deleteItem (item) {
        this.editedIndex = this.authors.indexOf(item)
        this.editedItem = Object.assign({}, item)
        this.dialogDelete = true
      },

      deleteItemConfirm () {
        let index = this.editedIndex;
        console.log(this.editedItem.id);
         axios
          .delete(process.env.VUE_APP_ROOT_API+"author/" + this.editedItem.id,{
            headers: { Authorization: "Bearer " + this.$jwt },
          })
          .then(() => {
            this.authors.splice(index, 1);
          }).catch((error) => {
            console.log(error);
          })
        
        this.closeDelete()
      },

      close () {
        this.dialog = false
        this.hideDiv = false
        this.$nextTick(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
        })
      },

      closeDelete () {
        this.dialogDelete = false
        this.$nextTick(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
        })
      },

      save (id) {
        this.$refs.form.validate();
        if (this.editedIndex > -1) {
          this.edit(id);
          
        } else {
          this.add();
        }
        this.close()
      },
      edit(id){
        let item = this.editedItem;
        let index = this.editedIndex;
        console.log(index)
        axios
            .put(process.env.VUE_APP_ROOT_API+"author/" + id, this.editedItem,{
              headers: { Authorization: "Bearer " + this.$jwt },
            })
            .then(() => {
              Object.assign(this.authors[index], item)
            }).catch((error)=>{
               this.$errorWrite(error);
            })  
      },
      add(){
        let item = this.editedItem;
        axios
            .post(process.env.VUE_APP_ROOT_API+"author", item, {
              headers: { Authorization: "Bearer " + this.$jwt },
            })
            .then(() => {
              this.getLastId();
              let id = localStorage.getItem("lastId")
              item.id = parseInt(id) + 1;
              this.authors.push(item)
              console.log(item)
            }).catch((error)=>{
              this.$errorWrite(error);
            })  
      },
      updateItemsPerPage (number) {
        this.searchObj.perPage = number;
      },
      getLastId(){
          axios
            .get(process.env.VUE_APP_ROOT_API+"author",
            {
              headers: { Authorization: "Bearer " + this.$jwt },
            })
            .then((response)=>{
              let data = response.data.items;
              var idArray = [];
              for(let i of data){
                idArray.push(i.id);
              }
              var max = idArray.reduce(function(a, b) {
              return Math.max(a, b);
              }, 0);
              localStorage.setItem("lastId",max.toString());
            })
      },
      // handleFile(e){
      //   const image = e;
      //           const reader = new FileReader();
      //           reader.readAsDataURL(image);
      //           reader.onload = e =>{
      //               this.previewImage=e.target.result;
      //           };
      //    let formData = new FormData();
      //    formData.append('image', image);
      //     axios
      //     .post(process.env.VUE_APP_ROOT_API+'upload',formData,{
      //         headers: {'Content-Type': image.type},
      //     }).then((response)=>{
      //         this.hideDiv=true;
      //         this.editedItem.coverPhoto = response.data;
      //         console.log(this.editedItem.coverPhoto)
      //     }).catch((error)=>{
      //         console.log(error);
      //     })
      // },
      // removeAddedPhoto(){
      //   let photoForDelete = this.editedItem.coverPhoto;
      //   axios
      //     .delete(process.env.VUE_APP_ROOT_API+'upload/photo/delete/' + photoForDelete,{
      //       headers: { Authorization: "Bearer " + this.$jwt },
      //     })
      //     .then(()=>{
      //       console.log("Obrisana");
      //       this.hideDiv = false;
      //     })
      // }
    },
  }
</script>