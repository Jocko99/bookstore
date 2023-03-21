import axios from 'axios';

export const photoMixin = {
    data: () => ({
        previewImage:null,
    }),
    computed:{
        photoForRepreset(){
            return this.editedIndex === -1 ?  "" : "../assets/images/" + this.editedItem.coverPhoto
        }
    },
    methods:{
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
    }
}