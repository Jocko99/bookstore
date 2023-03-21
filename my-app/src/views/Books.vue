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
    :items="books"
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
        <v-toolbar-title>Books</v-toolbar-title>
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
                      v-model="editedItem.isbn"
                      :rules="[rules.required,rules.requiredLength]"
                      counter
                      maxlength="13"
                      label="ISBN"
                    ></v-text-field>
                  </v-col>
                  <v-col
                    cols="12"
                    sm="6"
                    md="4"
                  >
                    <v-text-field
                      v-model="editedItem.title"
                      :rules="[rules.required,rules.min]"
                      counter
                      maxlength="50"
                      label="Title"
                    ></v-text-field>
                  </v-col>
                  <v-col
                    cols="12"
                    sm="6"
                    md="4"
                  >
                    <v-textarea
                      v-model="editedItem.description"
                      :rules="[rules.required,rules.min]"
                      counter
                      maxlength="3000"
                      label="Description"
                    ></v-textarea>
                  </v-col>  
                  <v-col
                    cols="12"
                    sm="6"
                    md="4"
                  >
                    <v-text-field
                      v-model="editedItem.unitPrice"
                      :rules="[rules.required,rules.min]"
                      counter
                      maxlength="6"
                      label="Price"
                    ></v-text-field>
                  </v-col>
                  <v-col
                    cols="12"
                    sm="6"
                    md="4"
                  >
                    <v-text-field
                        type="number"
                      v-model="editedItem.stock"
                      :rules="[rules.required]"
                      counter
                      label="Stock"
                    ></v-text-field>
                  </v-col>
                  <v-col
                    cols="12"
                    sm="6"
                    md="4"
                  >
                    <v-text-field
                    type="date"
                      v-model="editedItem.releaseDate"
                      :rules="[rules.required]"
                      label="Release date"
                    ></v-text-field>
                  </v-col>
                  <v-col
                    cols="12"
                    sm="6"
                    md="4"
                  >
                  <v-autocomplete
                    v-model="editedItem.genreId"
                    :items="genres"
                    :rules="[rules.required]"
                    outlined
                    dense
                    chips
                    small-chips
                    label="Genres"
                  ></v-autocomplete>
                  </v-col>
                  <v-col
                    cols="12"
                    sm="6"
                    md="4"
                  >
                  <v-autocomplete
                    v-model="editedItem.authors"
                    :items="allAuthors"
                    :rules="[rules.required]"
                    :item-text="firstName"
                    outlined
                    dense
                    chips
                    small-chips
                    label="Authors"
                    multiple
                  ></v-autocomplete>
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
  <div class="text-center pt-2" v-if="books.length">
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
  export default {
    data: () => ({
      searchObj: {
        keyword: null,
        page: 1,
        perPage: 10
      },
      genres:[],
      books:[],
      valid:true,
      hideDiv:false,
      previewImage:null,
      pageCount: 1,
      dialog: false,
      dialogDelete: false,
      itemsPerPageArray: [5, 10, 15],
      warningError:'',
      lastId:0,
      headers: [
        {
          text: 'ISBN',
          align: 'start',
          value: 'isbn',
        },
        { text: 'Genre', value: 'genre' },
        { text: 'Title', value: 'title' },
        { text: 'Description', value: 'description' },
        { text: 'Price', value: 'unitPrice' },
        { text: 'Stock', value: 'stock' },
        { text: 'Release date', value: 'releaseDate'},
        // { text: 'Authors', value: 'authors' },
        { text: 'Actions', value: 'actions', sortable: false },
      ],
      editedIndex: -1,
      defaultItem: {
        id:0,
        isbn:"",
        genreId:"",
        title:"",
        description:"",
        unitPrice:"",
        stock:0,
        releaseDate:"",
        authors:[],

      },
      rules: {
          required: value => !!value || 'This field is required.',
          requiredLength: value => value.length === 13 || 'ISBN need to has 13 character.',
          min: value => value.length >= 3 || 'Minimum length is 3.',
          photo: value => !value || value.size < 2000000 || 'Image size should be less than 2 MB!',
      },
      editedItem: {
        id:0,
        isbn:"",
        genreId:"",
        title:"",
        description:"",
        unitPrice:"",
        stock:0,
        releaseDate:"",
        authors:[],
      },
    }),
    computed: {
      formTitle () {
        return this.editedIndex === -1 ? 'New item' : 'Edit item'
      },
      photoForRepreset(){
          return this.editedIndex === -1 ?  "" : "../assets/images/" + this.editedItem.coverPhoto
      }
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
      this.getGenres()
      this.getAuthors();
    },

    methods: {
      initialize () {
         axios
          .get(process.env.VUE_APP_ROOT_API+"book", {
               headers: { Authorization: "Bearer " + this.$jwt},
               params: this.searchObj 
               }).then((response) => {
                response.data.items.forEach(element => {
                  var tmpAuthors = [];
                  element.authors.forEach(autors => {
                    var tmpObj = new Object();
                    tmpObj.text = autors.firstName + " " + autors.lastName
                    tmpObj.value = autors.id;
                    tmpAuthors.push(tmpObj);
                  })
                  element.authors = tmpAuthors;
                });
            this.books = response.data.items;
            this.page = response.data.page;
            this.pageCount = response.data.pagesCount;
          }).catch((error) =>{
            console.log(error);
          });
      },
      getAuthors(){
        axios
          .get(process.env.VUE_APP_ROOT_API+"author", {
               headers: { Authorization: "Bearer " + this.$jwt},
               params: this.searchObj 
               }).then((response) => {
                var tmpAuthors = [];
                response.data.items.forEach(element => {
                  var tmpObj = new Object();
                  tmpObj.text = element.firstName + " " + element.lastName
                  tmpObj.value = element.id;
                  tmpAuthors.push(tmpObj);
                });
            this.allAuthors = tmpAuthors;   
            }).catch((error) =>{
            console.log(error);
          });
      },
      editItem(item){  
        this.editedIndex = this.books.indexOf(item)
        this.editedItem = Object.assign({}, item)
        console.log(this.editedItem)
        this.hideDiv = true;
        // this.navigationId = item.id;
        this.dialog = true
      },
      deleteItem (item) {
        this.editedIndex = this.books.indexOf(item)
        this.editedItem = Object.assign({}, item)
        this.dialogDelete = true
      },

      deleteItemConfirm () {
        let index = this.editedIndex;
        console.log(this.editedItem.id);
         axios
          .delete(process.env.VUE_APP_ROOT_API+"book/" + this.editedItem.id,{
            headers: { Authorization: "Bearer " + this.$jwt },
          })
          .then(() => {
            this.books.splice(index, 1);
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
        var tmpAuthors = [];
        item.authors.forEach(element => {
          var tmpObject = new Object();
          tmpObject.authorId = element;
          tmpAuthors.push(tmpObject);
        });
        item.authors = [...tmpAuthors];
        console.log(index)
        axios
            .put(process.env.VUE_APP_ROOT_API+"book/" + id, this.editedItem,{
              headers: { Authorization: "Bearer " + this.$jwt },
            })
            .then(() => {
              Object.assign(this.books[index], item)
            }).catch((error)=>{
               this.$errorWrite(error);
            })  
      },
      add(){
        let item = this.editedItem;
        delete item.id;
        var tmpAuthors = [];
        item.authors.forEach(element => {
          var tmpObject = new Object();
          tmpObject.authorId = element;
          tmpAuthors.push(tmpObject);
        });
        item.authors = [...tmpAuthors];
        axios
            .post(process.env.VUE_APP_ROOT_API+"book", item, {
              headers: { Authorization: "Bearer " + this.$jwt },
            })
            .then(() => {
              this.getLastId();
              let id = localStorage.getItem("lastId")
              item.id = parseInt(id) + 1;
              this.books.push(item)
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
            .get(process.env.VUE_APP_ROOT_API+"book",
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
      handleFile(e){
        const image = e;
                const reader = new FileReader();
                reader.readAsDataURL(image);
                reader.onload = e =>{
                    this.previewImage=e.target.result;
                };
         let formData = new FormData();
         formData.append('image', image);
          axios
          .post(process.env.VUE_APP_ROOT_API+'upload',formData,{
              headers: {'Content-Type': image.type},
          }).then((response)=>{
              this.hideDiv=true;
              this.editedItem.coverPhoto = response.data;
              console.log(this.editedItem.coverPhoto)
          }).catch((error)=>{
              console.log(error);
          })
      },
      removeAddedPhoto(){
        let photoForDelete = this.editedItem.coverPhoto;
        axios
          .delete(process.env.VUE_APP_ROOT_API+'upload/photo/delete/' + photoForDelete,{
            headers: { Authorization: "Bearer " + this.$jwt },
          })
          .then(()=>{
            console.log("Obrisana");
            this.hideDiv = false;
          })
      },
      getGenres(){
          axios
          .get(process.env.VUE_APP_ROOT_API+'genre',{
            headers: { Authorization: "Bearer " + this.$jwt },
          })
          .then((response)=>{
            var genresTmp = [];
            response.data.items.forEach(element => {
              var tmpObj = new Object();
              tmpObj.text = element.name;
              tmpObj.value = element.id;
              genresTmp.push(tmpObj);
            })
            this.genres = genresTmp;
            console.log(this.genres)
          })
          
      }
    },
  }
</script>