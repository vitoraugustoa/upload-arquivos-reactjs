import React, { Component } from 'react';
import { uniqueId } from 'lodash';

import Api from './services/api';

import GlobalStyle from './styles/global';
import { Container, Content } from './styles';

import Upload from './components/Upload';
import FileList from './components/FileList/index';

class App extends Component {
  state = {
    uploadedFiles: [],
  };

 async componentDidMount() {
    const response = await Api.get("arquivo");

    this.setState({
      uploadedFiles: response.data.map(file => ({
        id: file.id,
        name: file.name,
        readableSize: file.size/1000,
        preview: file.url,
        uploaded: true,
        url: file.url,
      }))
    })
  };

  componentWillUnmount() {
    this.state.uploadedFiles.forEach(file => URL.revokeObjectURL(file.preview));
  };

  handleUpload = files => {
      const uploadedFiles = files.map(file => ({
         file,
         id: uniqueId('id_'),
         name : file.name,
         readableSize: file.size/1000,
         preview: URL.createObjectURL(file),
         progress: 0,
         uploaded: false,
         error: false,
         url: null,
      }));

      this.setState({
        uploadedFiles: this.state.uploadedFiles.concat(uploadedFiles)
      });

      uploadedFiles.forEach(this.processUpload);
  };

  updateFile = (id, data) => {
    this.setState({uploadedFiles: this.state.uploadedFiles.map(uploadedFile => {
      return id === uploadedFile.id ? {... uploadedFile, ...data } : uploadedFile;
    }) 
  })
  };

  processUpload = ( uploadedFile ) => {
      const data = new FormData();

      data.append("files",uploadedFile.file);

      Api.post('arquivo/UploadArquivos',data, {
        onUploadProgress: e => {
          const progress = parseInt(Math.round((e.loaded * 100) / e.total));
          this.updateFile(uploadedFile.id, {
            progress,
          })
        }
      }).then((response) => {
        this.updateFile(uploadedFile.id, {
          uploaded: true,
          id: response.data.id,
          url: response.data.url
        });
      }).catch(() => {
        this.updateFile(uploadedFile.id, {
          error:true        
        });
      });
  };

  handleDelete = async id => {
    await Api.delete(`arquivo/${id}`);

    this.setState({
      uploadedFiles: this.state.uploadedFiles.filter(file => file.id != id),
    });
  };

  render() {
    const { uploadedFiles } = this.state;

    return (
        <Container>
          <Content>
              <Upload onUpload={this.handleUpload}/>
              { !!uploadedFiles.length && (   // !! return true or false 
                <FileList files={uploadedFiles} onDelete={this.handleDelete} />    
              )}
          </Content>
          <GlobalStyle></GlobalStyle>
        </Container>
        
    );
  }
}

export default App;
