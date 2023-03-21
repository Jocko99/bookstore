<template>
    <div>
    <v-system-bar
      dark
      color="blue-grey" app
    >
      <v-spacer></v-spacer>
      <v-btn text @click="this.logout" v-if="this.$loggedUser">
        <span>Exit</span>
        <v-icon>mdi-logout</v-icon>    
      </v-btn>
    </v-system-bar>
  </div>
</template>

<script>
import {logoutMixin} from '../../mixins/logoutMixins';
export default {
    mixins:[logoutMixin],
}
</script>